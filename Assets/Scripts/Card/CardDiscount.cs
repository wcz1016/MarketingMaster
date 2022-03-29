using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDiscount : CardStrategy
{
    public float DiscountRate;

    public override void Execution(PlayerIndex index)
    {
        GameData instance = instances[(int)index];
        int CurCustLev = instance.counsumptionLevel;
        custlev = -(int)(CurCustLev * (1 - DiscountRate));
        base.Execution(index);
    }
}
