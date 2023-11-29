using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "RunVariables", menuName = "ScriptableObjects/RunVariables")]
public class RunVariables : ScriptableObject
{
    // Tracks dynamic values for each Run
    [SerializeField]
    public ABCConsumable[] consumableObjects;
    public int performancePoints;

    public void Init()
    {
        // start of whole game, not level
        consumableObjects = new ABCConsumable[]
        { new KitKat(20, 5), new Coffee(4, 10), new Adderall(3, 15), new Adderall(1, 20) };
    }
}

public enum EvadeType
{
    Dash,
    Parry
}

public enum ConsumableType
{
    KitKat = 0,
    Coffee = 1,
    Adderall = 2,
    Starman = 3,
}