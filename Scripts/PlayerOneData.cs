﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneData : Gamedata
{
    public static PlayerOneData instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
