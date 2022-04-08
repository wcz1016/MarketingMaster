using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyCard : Card
{
    private int _cost, _popularityAddition, _ratingAddition;
    private int _shopLevelAddition, _costPerRoundAddition;
    private int _consumptionLevelAddition, _customerFlowAddition;
    private float _consumptionLevelMultiplier, _customerFlowMultiplier;

    public int? Cost { get => _cost; set => _cost = value ?? 0; }
    public int? PopularityAddition { get => _popularityAddition; set => _popularityAddition = value ?? 0; }
    public int? RatingAddition { get => _ratingAddition; set => _ratingAddition = value ?? 0; }
    public int? ShopLevelAddition { get => _shopLevelAddition; set => _shopLevelAddition = value ?? 0; }
    public int? CostPerRoundAddition { get => _costPerRoundAddition; set => _costPerRoundAddition = value ?? 0; }

    public int? ConsumptionLevelAddition { get => _consumptionLevelAddition; set => _consumptionLevelAddition = value ?? 0; }
    public float? ConsumptionLevelMultiplier { get => _consumptionLevelMultiplier; set => _consumptionLevelMultiplier = value ?? 1.0f; }

    public int? CustomerFlowAddition { get => _customerFlowAddition; set => _customerFlowAddition = value ?? 0; }
    public float? CustomerFlowMultiplier { get => _customerFlowMultiplier; set => _customerFlowMultiplier = value ?? 1.0f; }

    public int Duration { get; set; }

    public override void Execute(PlayerIndex index)
    {
        GameData instance = getPlayerDataInstance(index);

        instance.cash -= _cost;
        instance.shopLevel += _shopLevelAddition;

        int customerFlowAdjustment = (int)(-instance.customerFlow * (1 - _customerFlowMultiplier))
                                        + _customerFlowAddition;
        int consumptionLevelAdjustment = (int)(-instance.consumptionLevel * (1 - _consumptionLevelMultiplier))
                                        + _consumptionLevelAddition;
        instance.AddCardBuff(new CardBuff(_popularityAddition, _ratingAddition, _costPerRoundAddition,
                                            consumptionLevelAdjustment, customerFlowAdjustment, Duration));      
    }
}
