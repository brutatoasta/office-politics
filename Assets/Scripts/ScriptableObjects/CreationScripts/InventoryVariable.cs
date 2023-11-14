using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "InventoryVariable", menuName = "ScriptableObjects/InventoryVariable", order = 4)]
public class InventoryVariable : ScriptableObject
{

    public ABCConsumable[] consumableObjects;
    public EvadeType evadeType;
}

public enum EvadeType {
    Dash,
    Parry
}