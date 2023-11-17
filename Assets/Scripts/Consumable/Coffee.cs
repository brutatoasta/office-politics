using System;
using System.Collections;
using UnityEngine;


public class Coffee : ABCConsumable
{
    public Coffee(ConsumableData initdata)
    {
        data = initdata;

    }
    public override void ConsumeEffect()
    {
        Debug.Log("Drank Coffee");
    }
}
