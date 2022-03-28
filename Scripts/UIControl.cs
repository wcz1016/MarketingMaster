using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public GameObject showtimetext;
    public GameObject GameOverText;
    public GameObject Canvas;
    public Text roundNumText;

    [Space]
    public Transform LeftArea;
    public Text LeftRatingText;
    public Text LeftCoinText;
    public Text LeftHouseLvText;
    public Text LeftPopularText;

    [Space]
    public Transform RightArea;
    public Text RightRatingText;
    public Text RightCoinText;
    public Text RightHouseLvText;
    public Text RightPopularText;

    public Cardlist cardlist;
    private void Start() {
        LeftCoinText.text = PlayerOneData.instance.cash.ToString();
        LeftRatingText.text = PlayerOneData.instance.refer.ToString();
        LeftHouseLvText.text = PlayerOneData.instance.level.ToString();
        LeftPopularText.text = PlayerOneData.instance.popular.ToString();

        RightCoinText.text = PlayerTwoData.instance.cash.ToString();
        RightRatingText.text = PlayerTwoData.instance.refer.ToString();
        RightHouseLvText.text = PlayerTwoData.instance.level.ToString();
        RightPopularText.text = PlayerOneData.instance.popular.ToString();
    }
    public void ShowTime(){
        Canvas.SetActive(true);
        DestroyAllCards(LeftArea);
        DestroyAllCards(RightArea);
        showtimetext.SetActive(true);
        
        LeftCoinText.text += " + " + PlayerOneData.instance.profit.ToString();
        LeftRatingText.text = PlayerOneData.instance.refer.ToString();
        LeftHouseLvText.text = PlayerOneData.instance.level.ToString();
        LeftPopularText.text = PlayerOneData.instance.popular.ToString();

        RightCoinText.text += " + " + PlayerTwoData.instance.profit.ToString();
        RightRatingText.text = PlayerTwoData.instance.refer.ToString();
        RightHouseLvText.text = PlayerTwoData.instance.level.ToString();
        RightPopularText.text = PlayerOneData.instance.popular.ToString();


    }

    public void NewRound(){
        Canvas.SetActive(false);
        roundNumText.text = "回合" + GetComponent<GameControl>().roundsnum.ToString();
        LeftCoinText.text = PlayerOneData.instance.cash.ToString();
        RightCoinText.text = PlayerTwoData.instance.cash.ToString();
        showtimetext.SetActive(false);
        if(LeftArea.childCount == 0)
            draw_cards();
    }
    public void DestroyAllCards(Transform area){
        for (int i = 0; i < area.transform.childCount; i++) {  
            Destroy(area.transform.GetChild(i).gameObject); 
            
        }  
    }
    public void gameover(){
        Debug.Log("gameover");
        DestroyAllCards(LeftArea);
        DestroyAllCards(RightArea);
        GameOverText.SetActive(true);
        //啊这样好蠢……
        roundNumText.text = "";
        Application.Quit();
    }
    public void draw_cards(){
        List<GameObject> spawn = new List<GameObject>();
        for (int i = 0; i < cardlist.PlayingCard.Count; i++)
        {
            if (cardlist.PlayingCard[i].GetComponent<Card>() != null)
            {
                if (GetComponent<GameControl>().roundsnum >= cardlist.PlayingCard[i].GetComponent<Card>().start&&
                    GetComponent<GameControl>().roundsnum <= cardlist.PlayingCard[i].GetComponent<Card>().end)
                {
                    spawn.Add(cardlist.PlayingCard[i]);
                }
            }
            
        }
        for (int i = 0; i < 3; i++){
            Instantiate(spawn[Random.Range(0, spawn.Count)], LeftArea);
            Instantiate(spawn[Random.Range(0, spawn.Count)], RightArea);
        }
    }

    public void initCard(){
        roundNumText.text = "回合" + GetComponent<GameControl>().roundsnum.ToString();
        showtimetext.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            Instantiate(cardlist.addrCard[Random.Range(0, cardlist.addrCard.Count)], LeftArea);
            Instantiate(cardlist.addrCard[Random.Range(0, cardlist.addrCard.Count)], RightArea);
        }
    }

    public void destroyAllOtherCards(int index, Transform area){
        for(int i = 0; i < 3; i++){
            if(i != index){
                Destroy(area.GetChild(i).gameObject);
            }
        }
    }
}
