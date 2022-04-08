using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBuff
{   
    //暂定持续11回合为永久持续
    public int Duration;
    public int PopularityAdjustment, RatingAdjustment;
    public int CostPerRoundAdjustment, ConsumptionLevelAdjustment, CustomerFlowAdjustment;

    public CardBuff(int popularityAdjustmentlar, int ratingAdjustment, 
        int costPerRoundAdjustment, int consumptionLevelAdjustment, 
        int customerFlowAdjustment, int duration)
    {
        this.PopularityAdjustment = popularityAdjustmentlar;
        this.RatingAdjustment = ratingAdjustment;
        this.CostPerRoundAdjustment = costPerRoundAdjustment;
        this.ConsumptionLevelAdjustment = consumptionLevelAdjustment;
        this.CustomerFlowAdjustment = customerFlowAdjustment;
        this.Duration = duration;
    }
}
