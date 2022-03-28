using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRival : CardStrategy
{
    [Header("以下是对对手的影响")]
    public int enemy_popular;
    public int enemy_refer, enemy_cost, enemy_custlev, enemy_people;

    public override void Execution(PlayerIndex index)
    {
        base.Execution(index);
        Gamedata instance = instances[1 - (int)index];
        instance.buffs.Add(new CardBuff(popular, refer, cost, custlev, people, duration));
        //instance.cash -= cash;
        instance.cost += enemy_cost;
        instance.popular += enemy_popular;
        instance.refer += enemy_refer;
        //instance.level += level;
        instance.custLv += enemy_custlev;
        instance.people += enemy_people;
    }
}
