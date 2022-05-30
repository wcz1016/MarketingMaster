using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    enum GameState { StartGame, StartRound, InRound, EndRound, GameOver }

    public delegate void OnShowTimeStart(float leftProbability);
    public static event OnShowTimeStart onShowTimeStart;

    public delegate void OnShowTimeEnd();
    public static event OnShowTimeEnd onShowTimeEnd;

    public static GameControl Instance;

    [Tooltip("展示阶段的持续时间")]
    public float DisplayDuration;
    public float ShowTimeDuration;
    [Tooltip("结束回合数")]
    public int WinningRounds;
    public GameObject Canvas;
    public GameObject StartTip;

    public List<KeyCode> LeftCardKeys;
    public List<KeyCode> RightCardKeys;
 
    [HideInInspector]
    public int RoundsNum = 0;

    private GameState _gameState;
    private bool _leftHasSelected, _rightHasSelected;
    private bool _leftHasExecuted, _rightHasExecuted;

    private bool _isShowtime;

    //public PeopleGeneration peopleController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        _gameState = GameState.StartGame;
    }

    void Update()
    {
        switch(_gameState)
        {
            case GameState.StartGame:
                if (Input.anyKeyDown)
                {
                    _gameState = GameState.StartRound;
                    Canvas.GetComponent<UIManager>().enabled = true;
                    Destroy(StartTip);
                }
                break;
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
            case GameState.GameOver:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
            OnGameOver();
        } else
        {
            _gameState = GameState.StartRound;
        }  
    }

    void StartNewRound()
    {
        _leftHasExecuted = false;
        _rightHasExecuted = false;

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

    private void OnGameOver()
    {
        Debug.Log("gameover");

        SoundManager.Instance.WinGamePlay();
        
        CardManager.Instance.DestroyAllCards(PlayerIndex.PlayerOne);
        CardManager.Instance.DestroyAllCards(PlayerIndex.PlayerTwo);

        UIManager.Instance.GameOver();
        GameObject.FindObjectOfType<FolkGenerator>().enabled = false;
        _gameState = GameState.GameOver;
    }
}
