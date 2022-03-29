using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoData : GameData
{
    public static PlayerTwoData instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
} 
