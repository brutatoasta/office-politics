using System.Collections;
using UnityEngine;


public class Coffee : ABCConsumable
{
    // Constructor to set count/ sprite
    public Coffee(int initCount, int initCost)
    {
        count = initCount;
        cost = initCost;
        LoadSprite("Consumables/Coffee");
    }

    // Implementation of abstract class for effect
    public override void ConsumeEffect()
    {
        GameManager.instance.StartCoroutine(TempIncreaseMovementSpeed());
        // Invoke Side effects (particles etc.)
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.Coffee);
    }

    // Coroutine to temp increase movement speed
    IEnumerator TempIncreaseMovementSpeed()
    {
        GameManager.instance.playerConstants.moveSpeed += 10;
        GameManager.instance.playerConstants.maxMoveSpeed += 10;
        yield return new WaitForSecondsRealtime(5f);
        GameManager.instance.playerConstants.moveSpeed -= 10;
        GameManager.instance.playerConstants.maxMoveSpeed -= 10;
    }
}
