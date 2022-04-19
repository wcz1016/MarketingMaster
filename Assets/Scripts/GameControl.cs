using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class GameControl : MonoBehaviour
{
    enum GameState { StartRound, InRound, EndRound }

    [Tooltip("展示阶段的持续时间")]
    public float waittime1, waittime2;
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
        //PeopleGeneration.instance.changeToMIddle();
        yield return new WaitForSeconds(waittime1);
        PlayerOneData.Instance.Roundover();
        PlayerTwoData.Instance.Roundover();
        
        if (PlayerOneData.Instance.cash > PlayerTwoData.Instance.cash){
            Debug.Log("PlayerOne won this round");
            //PeopleGeneration.instance.changeTarget(0);
        }
        else if(PlayerOneData.Instance.cash < PlayerTwoData.Instance.cash ){
            Debug.Log("PlayerTwo won this round");
            //PeopleGeneration.instance.changeTarget(1);
        }
            
        CardManager.Instance.DestroyAllCards(PlayerIndex.PlayerOne);
        CardManager.Instance.DestroyAllCards(PlayerIndex.PlayerTwo);

        Debug.Log("Showtime started");
        UIManager.Instance.ShowTime();
        SoundManager.Instance.CheerPlay();
        yield return new WaitForSeconds(waittime2);

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
            if (_leftCardIndex == -1)
            {
                for(int i = 0; i < LeftCardKeys.Count; i++)
                {
                    if (Input.GetKeyDown(LeftCardKeys[i]))
                    {
                        _leftCardIndex = i;
                        SoundManager.Instance.CardSelPlay();
                        break;
                    }
                }
            }

            if (_leftCardIndex != -1)
            {
                CardManager.Instance.ExecuteCard(_leftCardIndex, PlayerIndex.PlayerOne);
                _leftHasExecuted = true;
            }
        }

        if (!_rightHasExecuted)
        {
            if (_rightCardIndex == -1)
            {
                for (int i = 0; i < RightCardKeys.Count; i++)
                {
                    if (Input.GetKeyDown(RightCardKeys[i]))
                    {
                        _rightCardIndex = i;
                        SoundManager.Instance.CardSelPlay();
                        break;
                    }
                }
            }

            if (_rightCardIndex != -1)
            {
                CardManager.Instance.ExecuteCard(_rightCardIndex, PlayerIndex.PlayerTwo);
                _rightHasExecuted = true;
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
