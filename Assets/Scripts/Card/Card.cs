using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card
{
    public string Name { get; set; }
    public string Description { get; set; }

    public abstract void Execute(PlayerIndex index);

    protected GameData getPlayerDataInstance(PlayerIndex index)
    {
        return index == PlayerIndex.PlayerOne ? PlayerOneData.instance : (GameData)PlayerTwoData.instance;
    }

    public override string ToString()
    {
        return $"Name: {Name}\n" +
            $"Description: {Description}";
    }
}
