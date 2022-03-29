using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerIndex { PlayerOne = 0, PlayerTwo = 1 }

public class GameData
{
    public int cash;
    public int popularity;
    public int rating;
    public int shopLevel;
    public int costPerRound;
    public int counsumptionLevel;
    public int customerFlow;

    public List<CardBuff> buffs = new List<CardBuff>();
    // Start is called before the first frame update
    void Start()
    {
        cash = 20000;
        rating = 10;
    }

    public void Roundover()
    {
        costPerRound = (int)(customerFlow * Mathf.Sqrt(shopLevel) * 6);
        if (GameControl.roundsnum == 2) popularity = customerFlow * 5;
        popularity += customerFlow / 10 * (rating + 5) * shopLevel;
        rating += shopLevel;
        int profit = popularity * (counsumptionLevel + shopLevel * shopLevel) - costPerRound;
        cash += profit;

        for(int i = buffs.Count - 1; i >= 0; i--){
            buffs[i].duration--;
            if(buffs[i].duration == 0){
                CardBuff buff = buffs[i];

                popularity -= buff.popular;
                costPerRound -= buff.cost;
                rating -= buff.refer;
                counsumptionLevel -= buff.custlev;
                customerFlow -= buff.people;

                buffs.Remove(buff);
            }
        }
    }
}
