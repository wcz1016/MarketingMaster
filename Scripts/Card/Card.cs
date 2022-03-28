using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public int start, end;
    public enum PlayerIndex{PlayerOne = 0, PlayerTwo = 1}
    protected List<Gamedata> instances = new List<Gamedata>{PlayerOneData.instance, PlayerTwoData.instance};
    public abstract void Execution(PlayerIndex index);
}
