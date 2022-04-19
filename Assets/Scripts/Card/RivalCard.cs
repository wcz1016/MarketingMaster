using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalCard : StrategyCard
{
    private int _rivalCost, _rivalPopularityAddition, _rivalRatingAddition;
    private int _rivalShopLevelAddition, _rivalCostPerRoundAddition;
    private int _rivalConsumptionLevelAddition, _rivalCustomerFlowAddition;

    public int? RivalCost { get => _rivalCost; set => _rivalCost = value ?? 0; }
    public int? RivalPopularityAddition { get => _rivalPopularityAddition; set => _rivalPopularityAddition = value ?? 0; }
    public int? RivalRatingAddition { get => _rivalRatingAddition; set => _rivalRatingAddition = value ?? 0; }
    public int? RivalShopLevelAddition { get => _rivalShopLevelAddition; set => _rivalShopLevelAddition = value ?? 0; }
    public int? RivalCostPerRoundAddition { get => _rivalCostPerRoundAddition; set => _rivalCostPerRoundAddition = value ?? 0; }

    public int? RivalConsumptionLevelAddition { get => _rivalConsumptionLevelAddition; set => _rivalConsumptionLevelAddition = value ?? 0; }
    public int? RivalCustomerFlowAddition { get => _rivalConsumptionLevelAddition; set => _rivalConsumptionLevelAddition = value ?? 0; }

    public override void Execute(PlayerIndex index)
    {
        base.Execute(index);
        GameData rivalInstance = index == PlayerIndex.PlayerOne ? 
            (GameData)PlayerTwoData.Instance : PlayerOneData.Instance;
        rivalInstance.AddCardBuff(new CardBuff(_rivalPopularityAddition, _rivalRatingAddition, _rivalCostPerRoundAddition, _rivalConsumptionLevelAddition,
            _rivalCustomerFlowAddition, Duration));
    }
}
