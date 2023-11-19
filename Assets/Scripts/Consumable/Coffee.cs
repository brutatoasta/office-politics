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
        GameManager.instance.StartCoroutine(TempIncreaseMovementSpeed());
    }
    IEnumerator TempIncreaseMovementSpeed()
    {
        GameManager.instance.playerConstants.moveSpeed += 10;
        GameManager.instance.playerConstants.maxMoveSpeed += 10;
        yield return new WaitForSecondsRealtime(20);
        GameManager.instance.playerConstants.moveSpeed -= 10;
        GameManager.instance.playerConstants.maxMoveSpeed -= 10;
    }
}
