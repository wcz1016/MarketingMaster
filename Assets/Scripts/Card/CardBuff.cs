using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBuff
{   
    //暂定持续11回合为永久持续
    public const int INFINITY = 11;
    public int duration = INFINITY;
    public int popular;
    public int refer, cost, custlev, people;
    public CardBuff(int popular, int refer, int cost, int custlev, int people, int duration = INFINITY){
        this.popular = popular;
        this.refer = refer;
        this.cost = cost;
        this.custlev = custlev;
        this.people = people;
        this.duration = duration;
    }
}
