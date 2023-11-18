using System;
using System.Collections;
using UnityEngine;


public class KitKat : ABCConsumable
{

    public KitKat(int initCount)
    {
        count = initCount;
        LoadSprite("Paper-document-4 (2)"); // TODO: replace sprite
    }
    public override void ConsumeEffect()
    {
        Debug.Log("Ate KitKat");
    }

}
