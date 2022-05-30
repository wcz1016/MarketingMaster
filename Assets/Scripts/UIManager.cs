using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject ShowtimeText;
    public GameObject GameOverText;
    public GameObject WhiteMask;
    public GameObject RoundNumText;
    
    [Space] 
    public Text LeftRatingText;
    public Text LeftCoinText;
    public Text LeftHouseLvText;
    public Text LeftPopularText;
    public GameObject LeftpopularImg;

    [Space]
    public Text RightRatingText;
    public Text RightCoinText;
    public Text RightHouseLvText;
    public Text RightPopularText;
    public GameObject RightPopularImg;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start() {
        LeftCoinText.text = PlayerOneData.Instance.cash.ToString();
        LeftRatingText.text = PlayerOneData.Instance.rating.ToString();
        LeftHouseLvText.text = PlayerOneData.Instance.shopLevel.ToString();
        LeftPopularText.text = PlayerOneData.Instance.popularity.ToString();

        RightCoinText.text = PlayerTwoData.Instance.cash.ToString();
        RightRatingText.text = PlayerTwoData.Instance.rating.ToString();
        RightHouseLvText.text = PlayerTwoData.Instance.shopLevel.ToString();
        RightPopularText.text = PlayerOneData.Instance.popularity.ToString();  
    }

    public void ShowTime(){
        WhiteMask.SetActive(true);

        ShowtimeText.SetActive(true);
        LeftpopularImg.SetActive(true);
        RightPopularImg.SetActive(true);
        
        LeftCoinText.text += " + " + PlayerOneData.Instance.CalProfit().ToString();
        LeftRatingText.text = PlayerOneData.Instance.rating.ToString();
        LeftHouseLvText.text = PlayerOneData.Instance.shopLevel.ToString();
        LeftPopularText.text = PlayerOneData.Instance.popularity.ToString();

        RightCoinText.text += " + " + PlayerTwoData.Instance.CalProfit().ToString();
        RightRatingText.text = PlayerTwoData.Instance.rating.ToString();
        RightHouseLvText.text = PlayerTwoData.Instance.shopLevel.ToString();
        RightPopularText.text = PlayerTwoData.Instance.popularity.ToString();
    }

    public void StartNewRound(){
        WhiteMask.SetActive(false);

        LeftpopularImg.SetActive(false);
        RightPopularImg.SetActive(false);

        RoundNumText.GetComponent<Text>().text = "回合" + (GameControl.Instance.RoundsNum + 1).ToString();

        LeftCoinText.text = PlayerOneData.Instance.cash.ToString();
        RightCoinText.text = PlayerTwoData.Instance.cash.ToString();

        ShowtimeText.SetActive(false);
    }
    
    public void GameOver() {   
        GameOverText.SetActive(true);
        RoundNumText.SetActive(false);
        ShowtimeText.SetActive(false);

        if (PlayerOneData.Instance.cash > PlayerTwoData.Instance.cash)
        {
            GameOverText.GetComponent<Text>().text = "玩家一胜利";
        }
        else if (PlayerOneData.Instance.cash < PlayerTwoData.Instance.cash)
        {
            GameOverText.GetComponent<Text>().text = "玩家二胜利";
        }
        else
        {
            GameOverText.GetComponent<Text>().text = "平局";
        }
    }
}
