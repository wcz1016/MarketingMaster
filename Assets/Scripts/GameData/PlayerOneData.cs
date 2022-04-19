using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 这个单例是不是写得不太好？用复合可以代替继承吧？
public class PlayerOneData : GameData
{
    public static PlayerOneData Instance = null;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
