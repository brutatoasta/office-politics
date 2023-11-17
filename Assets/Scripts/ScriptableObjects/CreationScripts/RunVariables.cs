using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "RunVariables", menuName = "ScriptableObjects/RunVariables")]
public class RunVariables : ScriptableObject
{
    // Tracks dynamic values for each Run

    public ABCConsumable[] consumableObjects;
    public ConsumableData[] consumableDatas;

    public int performancePoints;

    public void Init()
    {
        // start of whole game, not level
        consumableObjects = new ABCConsumable[]
        { new KitKat(consumableDatas[0]), new Coffee(consumableDatas[1]), new KitKat(consumableDatas[0]) };
    }
}

public enum EvadeType
{
    Dash,
    Parry
}