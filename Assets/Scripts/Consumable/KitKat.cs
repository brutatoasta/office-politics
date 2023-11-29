using System;
using System.Collections;
using UnityEngine;


public class KitKat : ABCConsumable
{

    public KitKat(int initCount, int initCost)
    {
        count = initCount;
        cost = initCost;
        LoadSprite("Consumables/Choco"); // TODO: replace sprite
    }
    public override void ConsumeEffect()
    {
        GameManager.instance.levelVariables.stressPoints = (GameManager.instance.levelVariables.stressPoints > 10)?
                                                                GameManager.instance.levelVariables.stressPoints - 10:
                                                                0;
        GameManager.instance.IncreaseStress();
    }

}
