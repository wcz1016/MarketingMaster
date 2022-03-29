using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    public abstract void Execution(PlayerIndex index);

    private GameData getPlayerDataInstance(PlayerIndex index)
    {
        return index == PlayerIndex.PlayerOne ? PlayerOneData.instance : (GameData)PlayerTwoData.instance;
    }
}
