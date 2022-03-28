using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStrategy : Card
{
    public const int INFINITY = 11;
    public int duration = INFINITY;
    public string cardDescription;

    
    public int popular, cash, level;
    public int refer, cost, custlev, people;

    //List<Gamedata> instances = new List<Gamedata>{PlayerOneData.instance, PlayerTwoData.instance};
    public override void Execution(PlayerIndex index){
        Gamedata instance = instances[(int)index];
        instance.buffs.Add(new CardBuff(popular, refer, cost, custlev, people, duration));
        instance.cash -= cash;
        instance.cost += cost;
        instance.popular += popular;
        instance.refer += refer;
        instance.level += level;
        instance.custLv += custlev;
        instance.people += people;

    }
}
