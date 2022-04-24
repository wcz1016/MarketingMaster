using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class GameControl : MonoBehaviour
{
    enum GameState { StartRound, InRound, EndRound }

    public delegate void OnShowTimeStart(float leftProbability);
    public static event OnShowTimeStart onShowTimeStart;

    public delegate void OnShowTimeEnd();
    public static event OnShowTimeEnd onShowTimeEnd;

    [Tooltip("展示阶段的持续时间")]
    public float DisplayDuration;
    public static float ShowTimeDuration = 3;
    [Tooltip("结束回合数")]
    public static int WinningRounds = 11;
    public GameObject PeopleControl;
    public GameObject Canvas;
    public GameObject StartTip;

    public List<KeyCode> LeftCardKeys;
    public List<KeyCode> RightCardKeys;
 
    [HideInInspector]
    // 设计成静态变量是不是有问题？
    public static int RoundsNum = 0;

    private GameState _gameState;
    private bool _leftHasSelected, _rightHasSelected;
    private bool _leftHasExecuted, _rightHasExecuted;
    // selected card index, -1 means not selected any
    private int _leftCardIndex = -1, _rightCardIndex = -1;
    private bool _isShowtime;

    private bool _showingTips;

    //public PeopleGeneration peopleController;
    
    void Start()
    {
        _showingTips = true;
        _gameState = GameState.StartRound;
    }

    void Update()
    {
        if (_showingTips)
        {
            if (Input.anyKeyDown)
            {
                _showingTips = false;
                Canvas.GetComponent<UIManager>().enabled = true;
                Destroy(StartTip);
            }
            return;
        }

        switch(_gameState)
        {
            case GameState.StartRound:
                StartNewRound();
                break;
            case GameState.InRound:
                HandleInput();
                break;
            case GameState.EndRound:
                if (_isShowtime)
                {
                    _isShowtime = false;
                    StartCoroutine(EndRound());
                }
                break;
        }
    }

    IEnumerator EndRound()
    {
        yield return new WaitForSeconds(DisplayDuration);
        PlayerOneData.Instance.Roundover();
        PlayerTwoData.Instance.Roundover();
        
        //if (PlayerOneData.Instance.cash > PlayerTwoData.Instance.cash){
        //    Debug.Log("PlayerOne won this round");
        //}
        //else if(PlayerOneData.Instance.cash < PlayerTwoData.Instance.cash ){
        //    Debug.Log("PlayerTwo won this round");
        //}
            
        CardManager.Instance.DestroyAllCards(PlayerIndex.PlayerOne);
        CardManager.Instance.DestroyAllCards(PlayerIndex.PlayerTwo);

        //Debug.Log("Showtime started");

        float folkToLeftProbability = (float)PlayerOneData.Instance.CalProfit() /
            (PlayerOneData.Instance.CalProfit() + PlayerTwoData.Instance.CalProfit());
        Debug.Log(folkToLeftProbability);
        onShowTimeStart.Invoke(folkToLeftProbability);

        UIManager.Instance.ShowTime();
        SoundManager.Instance.CheerPlay();
        yield return new WaitForSeconds(ShowTimeDuration);
        onShowTimeEnd.Invoke();

        RoundsNum++;

        if (RoundsNum == 10)
        {
            SoundManager.Instance.ChangeBGM();
        }

        if (RoundsNum > WinningRounds)
        {
            GameOver();
        } else
        {
            _gameState = GameState.StartRound;
        }  
    }

    void StartNewRound()
    {
        _leftHasExecuted = false;
        _leftCardIndex = -1;
        _rightHasExecuted = false;
        _rightCardIndex = -1;

        CardManager.Instance.DrawCards(RoundsNum);

        SoundManager.Instance.CardAppearPlay();

        UIManager.Instance.StartNewRound();

        _gameState = GameState.InRound;
    }

    //负责处理一个回合的双方行动
     void HandleInput(){
        if (!_leftHasExecuted)
        {
            for(int i = 0; i < LeftCardKeys.Count; i++)
            {
                if (Input.GetKeyDown(LeftCardKeys[i]))
                {
                    SoundManager.Instance.CardSelPlay();
                    CardManager.Instance.ExecuteCard(i, PlayerIndex.PlayerOne);
                    _leftHasExecuted = true;
                    break;
                }
            }  
        }

        if (!_rightHasExecuted)
        {
            for (int i = 0; i < RightCardKeys.Count; i++)
            {
                if (Input.GetKeyDown(RightCardKeys[i]))
                {
                    SoundManager.Instance.CardSelPlay();
                    CardManager.Instance.ExecuteCard(i, PlayerIndex.PlayerTwo);
                    _rightHasExecuted = true;
                    break;
                }
            }
        }

        if (_leftHasExecuted && _rightHasExecuted)
        {
            _gameState = GameState.EndRound;
            _isShowtime = true;
        }    
    }

    private void GameOver()
    {
        Debug.Log("gameover");

        SoundManager.Instance.winGamePlay();
        
        CardManager.Instance.DestroyAllCards(PlayerIndex.PlayerOne);
        CardManager.Instance.DestroyAllCards(PlayerIndex.PlayerTwo);

        UIManager.Instance.GameOver();

        Application.Quit();
    }
}
