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
        GameData instance = instances[1 - (int)index];
        instance.buffs.Add(new CardBuff(popular, refer, cost, custlev, people, duration));
        //instance.cash -= cash;
        instance.costPerRound += enemy_cost;
        instance.popularity += enemy_popular;
        instance.rating += enemy_refer;
        //instance.level += level;
        instance.counsumptionLevel += enemy_custlev;
        instance.customerFlow += enemy_people;
    }
}
