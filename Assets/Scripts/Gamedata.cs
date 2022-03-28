using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamedata : MonoBehaviour
{
    public int cash;
    public int popular;
    public int refer;
    public int level;
    public int cost,addcost;
    public int custLv;
    public int people;
    public int profit;

    public List<CardBuff> buffs = new List<CardBuff>();
    // Start is called before the first frame update
    void Start()
    {
        cash = 20000;
        refer = 10;
    }

    public void Roundover()
    {
        cost = (int)(people * Mathf.Sqrt(level) * 6);
        if (GameControl.roundsnum == 2) popular = people * 5;
        popular += people / 10 * (refer + 5) * level;
        refer += level;
        profit = popular * (custLv + level * level) - cost - addcost;
        cash += profit;

        for(int i = buffs.Count - 1; i >= 0; i--){
            buffs[i].duration--;
            if(buffs[i].duration == 0){
                CardBuff buff = buffs[i];

                popular -= buff.popular;
                cost -= buff.cost;
                refer -= buff.refer;
                custLv -= buff.custlev;
                people -= buff.people;

                buffs.Remove(buff);
            }
        }
    }
}
