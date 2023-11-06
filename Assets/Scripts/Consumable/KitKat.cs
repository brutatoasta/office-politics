using System;
using System.Collections;
using UnityEngine;


public class KitKat : ABCConsumable
{
    public KitKat(int initCount, Sprite initSprite)
    {
        count = initCount;
        sprite = initSprite;
    }
    public override void ConsumeEffect()
    {
        Debug.Log("Ate KitKat");
    }
}
