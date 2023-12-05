using System;
using System.Collections;
using UnityEngine;


public class Coffee : ABCConsumable
{
    public Coffee(int initCount, int initCost)
    {
        count = initCount;
        cost = initCost;
        LoadSprite("Consumables/Coffee"); // TODO: replace sprite
    }
    public override void ConsumeEffect()
    {
        GameManager.instance.StartCoroutine(TempIncreaseMovementSpeed());
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.Coffee);
    }
    IEnumerator TempIncreaseMovementSpeed()
    {
        GameManager.instance.playerConstants.moveSpeed += 10;
        GameManager.instance.playerConstants.maxMoveSpeed += 10;
        yield return new WaitForSecondsRealtime(5f);
        GameManager.instance.playerConstants.moveSpeed -= 10;
        GameManager.instance.playerConstants.maxMoveSpeed -= 10;
    }
}
