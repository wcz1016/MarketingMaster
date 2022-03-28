using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAddress : Card
{
    public int cardRefer;
    public int level;
    public int cardCustlv;
    public int people;
    public int cardCost;
    
    
    public void CashCost(int cost, PlayerIndex index)
    {
        instances[(int)index].cash -= cost;
    }

    public void Referup(int cardRefer, PlayerIndex index)
    {
        instances[(int)index].refer += cardRefer;
    }

    public void LvSet(int level, PlayerIndex index)
    {
        instances[(int)index].level = level;
    }

    public void CustlvSet(int custLv, PlayerIndex index)
    {
        instances[(int)index].custLv = custLv;
    }

    public void People(int people, PlayerIndex index)
    {
        instances[(int)index].people = people;
    }

    public override void Execution(PlayerIndex index){
        CashCost(cardCost, index);
        Referup(cardRefer, index);
        LvSet(level, index);
        CustlvSet(cardCustlv, index);
        People(people, index);
        SpecialEffect();
        Debug.Log((int)index + " has executed " + gameObject.name);
    }
    public virtual void SpecialEffect()
    {

    }
}
