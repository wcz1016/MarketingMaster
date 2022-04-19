using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressCard : Card
{
    public int Cost { get; set; }
    public int Rating { get; set; }
    public int ShopLevel { get; set; }
    public int ConsumptionLevel { get; set; }
    public int CustomerFlow { get; set; }

    public override void Execute(PlayerIndex index){
        GameData instance = getPlayerDataInstance(index);

        instance.cash -= Cost;
        instance.rating = Rating;
        instance.shopLevel = ShopLevel;
        instance.consumptionLevel = ConsumptionLevel;
        instance.customerFlow = CustomerFlow;
        instance.popularity = CustomerFlow * 5;
    }

    public override string ToString()
    {
        return base.ToString() +
            $"Cost: {Cost}\n" +
            $"Rating: {Rating}\n" +
            $"ShopLevel: {ShopLevel}\n" +
            $"ConsumptionLevel: {ConsumptionLevel}\n" +
            $"CustomerFlow: {CustomerFlow}";
    }
}

