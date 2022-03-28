using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class GameControl : MonoBehaviour
{
    public Transform LeftArea;
    public Transform RightArea;
    [Space]
    [Tooltip("展示阶段的持续时间")]
    public float waittime1,waittime2;
    [Tooltip("结束回合数")]
    public int WinningRounds = 12;
    public GameObject PeopleControl;
    bool LeftHasSel = false, RightHasSel = false;//代表左右双方是否已经选择好了卡牌
    bool isshowtime, isround = true;
    bool hasdrawncards;
    [HideInInspector]
    public static int roundsnum = 1;
    
    //public PeopleGeneration peopleController;
    UIControl UIcontroler;
    
    //public PlayerOneData P1;
    //public PlayerTwoData P2;

    void Start()
    {
        UIcontroler = GetComponent<UIControl>();
        UIcontroler.initCard();
    }

    void Update()
    {
        if(isshowtime){
            isshowtime = false;
            StartCoroutine(EndRound());
        }
        if(isround){
            StartRound();    
        }
        if(roundsnum > WinningRounds){
            isshowtime = false;
            isround = false;
            UIcontroler.gameover();
        }
    }

    IEnumerator EndRound()
    {
        
        PeopleGeneration.instance.changeToMIddle();
        yield return new WaitForSeconds(waittime1);
        PlayerOneData.instance.Roundover();
        PlayerTwoData.instance.Roundover();
        
        if (PlayerOneData.instance.cash > PlayerTwoData.instance.cash){
            Debug.Log("PlayerOne won this round");
            PeopleGeneration.instance.changeTarget(0);
        }
        else if(PlayerOneData.instance.cash < PlayerTwoData.instance.cash ){
            Debug.Log("PlayerTwo won this round");
            PeopleGeneration.instance.changeTarget(1);
        }
            
        Debug.Log("Showtime started");
        
        UIcontroler.ShowTime();
        SoundManager.instance.CheerPlay();
        yield return new WaitForSeconds(waittime2);
        SoundManager.instance.CardAppearPlay();
        
        //开始下一轮回合
        LeftHasSel = false;
        RightHasSel = false;
        isround = true;

        UIcontroler.NewRound();
        

    }


    //负责处理一个回合的双方行动
     void StartRound(){
        
        if(!LeftHasSel){
            if(Input.GetKeyDown(KeyCode.A)){
                LeftHasSel = true;
                LeftArea.GetChild(0).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerOne);
                if (LeftArea.GetChild(0).GetComponent<Animator>() != null)
                    LeftArea.GetChild(0).GetComponent<Animator>().SetTrigger("issel");
                
                UIcontroler.destroyAllOtherCards(0, LeftArea);
                SoundManager.instance.CardSelPlay();
            }
            else if(Input.GetKeyDown(KeyCode.S)){
                LeftHasSel = true;
                LeftArea.GetChild(1).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerOne);
                if (LeftArea.GetChild(1).GetComponent<Animator>() != null)
                    LeftArea.GetChild(1).GetComponent<Animator>().SetTrigger("issel");

                UIcontroler.destroyAllOtherCards(1, LeftArea);
                SoundManager.instance.CardSelPlay();
            }
            else if(Input.GetKeyDown(KeyCode.D)){
                LeftHasSel = true;
                LeftArea.GetChild(2).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerOne);
                if (LeftArea.GetChild(2).GetComponent<Animator>() != null)
                    LeftArea.GetChild(2).GetComponent<Animator>().SetTrigger("issel");
                UIcontroler.destroyAllOtherCards(2, LeftArea);
                SoundManager.instance.CardSelPlay();
            }
            else if(Input.GetKeyDown(KeyCode.W)){
                LeftHasSel = true;
                UIcontroler.DestroyAllCards(LeftArea);
            }
        }
        
        if(!RightHasSel){
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                RightHasSel = true;
                RightArea.GetChild(0).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerTwo);
                if(RightArea.GetChild(0).GetComponent<Animator>()!=null)
                    RightArea.GetChild(0).GetComponent<Animator>().SetTrigger("issel");

                UIcontroler.destroyAllOtherCards(0, RightArea);
                SoundManager.instance.CardSelPlay();
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow)){
                RightHasSel = true;
                RightArea.GetChild(1).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerTwo);
                if (RightArea.GetChild(0).GetComponent<Animator>() != null)
                    RightArea.GetChild(1).GetComponent<Animator>().SetTrigger("issel");

                UIcontroler.destroyAllOtherCards(1, RightArea);
                SoundManager.instance.CardSelPlay();
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow)){
                RightHasSel = true;
                RightArea.GetChild(2).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerTwo);
                if (RightArea.GetChild(0).GetComponent<Animator>() != null)
                    RightArea.GetChild(2).GetComponent<Animator>().SetTrigger("issel");

                UIcontroler.destroyAllOtherCards(2, RightArea);
                SoundManager.instance.CardSelPlay();
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow)){
                RightHasSel = true;
                UIcontroler.DestroyAllCards(RightArea);
            }
        }
        
        if(LeftHasSel && RightHasSel){
            isshowtime = true;
            isround = false;
            roundsnum++;
        }

        if(roundsnum > 10){
            SoundManager.instance.ChangeBGM();
        }
            
    }
    


}
