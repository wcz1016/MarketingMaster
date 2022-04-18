using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoData : GameData
{
    public static PlayerTwoData Instance = null;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
} 
