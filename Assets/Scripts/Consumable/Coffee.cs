using System;
using System.Collections;
using UnityEngine;


public class Coffee : ABCConsumable
{
    public Coffee(int initCount)
    {
        count = initCount;
        LoadSprite("Paper-document-4 (2)"); // TODO: replace sprite
    }
    public override void ConsumeEffect()
    {
        Debug.Log("Drank Coffee");
    }
}
