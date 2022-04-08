using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerIndex { PlayerOne = 0, PlayerTwo = 1 }

public class GameData: MonoBehaviour
{
    public static int INFINITY = 11;

    public int cash;
    public int popularity;
    public int rating;
    public int shopLevel;
    public int costPerRound;
    public int consumptionLevel;
    public int customerFlow;

    public List<CardBuff> buffs = new List<CardBuff>();
    // Start is called before the first frame update
    void Start()
    {
        cash = 20000;
        rating = 10;
    }

    public int CalProfit()
    {
        return popularity * (consumptionLevel + shopLevel * shopLevel) - costPerRound;
    }

    public void AddCardBuff(CardBuff cardBuff)
    {
        buffs.Add(cardBuff);
        costPerRound += cardBuff.CostPerRoundAdjustment;
        popularity += cardBuff.PopularityAdjustment;
        rating += cardBuff.RatingAdjustment;
        consumptionLevel += cardBuff.ConsumptionLevelAdjustment;
        customerFlow += cardBuff.CustomerFlowAdjustment;
    }

    private void RemoveCardBuff(CardBuff cardBuff)
    {
        buffs.Remove(cardBuff);
        costPerRound -= cardBuff.CostPerRoundAdjustment;
        popularity -= cardBuff.PopularityAdjustment;
        rating -= cardBuff.RatingAdjustment;
        consumptionLevel -= cardBuff.ConsumptionLevelAdjustment;
        customerFlow -= cardBuff.CustomerFlowAdjustment;
    }

    public void Roundover()
    {
        costPerRound = (int)(customerFlow * Mathf.Sqrt(shopLevel) * 6);

        //???
        if (GameControl.roundsnum == 2) popularity = customerFlow * 5;

        popularity += customerFlow / 10 * (rating + 5) * shopLevel;
        rating += shopLevel;
        cash += CalProfit();

        for(int i = buffs.Count - 1; i >= 0; i--){
            buffs[i].Duration--;
            if(buffs[i].Duration == 0){
                RemoveCardBuff(buffs[i]);
            }
        }
    }
}
