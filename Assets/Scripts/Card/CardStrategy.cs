using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStrategy : Card
{
    public const int INFINITY = 11;
    public int duration = INFINITY;

    
    public int popular, cash, level;
    public int refer, cost, custlev, people;

    //List<Gamedata> instances = new List<Gamedata>{PlayerOneData.instance, PlayerTwoData.instance};
    public override void Execution(PlayerIndex index){
        GameData instance = instances[(int)index];
        instance.buffs.Add(new CardBuff(popular, refer, cost, custlev, people, duration));
        instance.cash -= cash;
        instance.costPerRound += cost;
        instance.popularity += popular;
        instance.rating += refer;
        instance.shopLevel += level;
        instance.counsumptionLevel += custlev;
        instance.customerFlow += people;

    }
}
