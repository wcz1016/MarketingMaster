using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card10 : CardStrategy
{
    public float Rate;

    public override void Execution(PlayerIndex index)
    {
        Gamedata instance = instances[(int)index];
        int CurPeople = instance.people;
        people = -(int)(CurPeople * (1 - Rate));
        base.Execution(index);
    }
}
