using System;
using System.Collections;
using UnityEngine;


public class Coffee : ABCConsumable
{
    public Coffee(int initCount, Sprite initSprite)
    {
        count = initCount;
        sprite = initSprite;
    }
    public override void ConsumeEffect()
    {
        Debug.Log("Drank Coffee");
    }
}
