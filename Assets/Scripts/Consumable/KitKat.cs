using System;
using System.Collections;
using UnityEngine;


public class KitKat : ABCConsumable
{
    public KitKat(ConsumableData initData)
    {
        data = initData;
    }
    public override void ConsumeEffect()
    {
        Debug.Log("Ate KitKat");
    }

}
