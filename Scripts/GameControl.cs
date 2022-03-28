using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour
{


    public Transform LeftArea;
    public Transform RightArea;
    [Space]
    [Tooltip("展示阶段的持续时间")]
    public float waittime1,waittime2;
    [Tooltip("结束回合数")]
    public int WinningRounds = 12;

    bool LeftHasSel = false, RightHasSel = false;//代表左右双方是否已经选择好了卡牌
    bool isshowtime, isround = true;
    bool hasdrawncards;
    [HideInInspector]
    public int roundsnum = 1;
    
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
        yield return new WaitForSeconds(waittime1);
        Debug.Log("Showtime started");
        PlayerOneData.instance.Roundover();
        PlayerTwoData.instance.Roundover();
        UIcontroler.ShowTime();
        yield return new WaitForSeconds(waittime2);

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
                UIcontroler.destroyAllOtherCards(0, LeftArea);
            }
            else if(Input.GetKeyDown(KeyCode.S)){
                LeftHasSel = true;
                LeftArea.GetChild(1).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerOne);
                UIcontroler.destroyAllOtherCards(1, LeftArea);
            }
            else if(Input.GetKeyDown(KeyCode.D)){
                LeftHasSel = true;
                LeftArea.GetChild(2).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerOne);
                UIcontroler.destroyAllOtherCards(2, LeftArea);
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
                UIcontroler.destroyAllOtherCards(0, RightArea);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow)){
                RightHasSel = true;
                RightArea.GetChild(1).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerTwo);
                UIcontroler.destroyAllOtherCards(1, RightArea);
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow)){
                RightHasSel = true;
                RightArea.GetChild(2).GetComponent<Card>().Execution(CardAddress.PlayerIndex.PlayerTwo);
                UIcontroler.destroyAllOtherCards(2, RightArea);
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
            
    }
    


}
