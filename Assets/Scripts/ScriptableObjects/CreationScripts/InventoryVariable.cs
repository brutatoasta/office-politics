using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "InventoryVariable", menuName = "ScriptableObjects/InventoryVariable", order = 4)]
public class InventoryVariable : ScriptableObject
{

    public ABCConsumable[] consumableObjects;
    public EvadeType evadeType;

    public TaskItem[] taskQuotas;

    public int stressPoint;
    public int maxStressPoint;
    public int sanityPoint;
    public int performancePoint;
}

public enum EvadeType {
    Dash,
    Parry
}