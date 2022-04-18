using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRival : StrategyCard
{
    [Header("以下是对对手的影响")]
    public int enemy_popular;
    public int enemy_refer, enemy_cost, enemy_custlev, enemy_people;

    public override void Execute(PlayerIndex index)
    {
        base.Execute(index);
        PlayerIndex rivalIndex = (index == PlayerIndex.PlayerOne) ? PlayerIndex.PlayerTwo : PlayerIndex.PlayerOne;
        GameData rivalInstance = getPlayerDataInstance(rivalIndex);
        //rivalInstance.buffs.Add(new CardBuff(PopularityAddition, RatingAddition, CostPerRoundAddtion, ConsumptionLevelAddition, CustomerFlowAddition, Duration));
        //instance.cash -= cash;
        rivalInstance.costPerRound += enemy_cost;
        rivalInstance.popularity += enemy_popular;
        rivalInstance.rating += enemy_refer;
        //instance.level += level;
        rivalInstance.consumptionLevel += enemy_custlev;
        rivalInstance.customerFlow += enemy_people;
    }
}
