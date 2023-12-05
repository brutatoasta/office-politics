using System.Collections;
using UnityEngine;


public class Starman : ABCConsumable
{
    // Constructor to set sprite/ count
    public Starman(int initCount, int initCost)
    {
        count = initCount;
        cost = initCost;
        LoadSprite("Consumables/SuperStar");
    }

    // Implementation of abstract class for effect
    public override void ConsumeEffect()
    {
        GameManager.instance.StartCoroutine(Invincibility());
        // Invoke Side effects (shader etc.)
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.Starman);
    }

    // Corotine for making player invincible
    IEnumerator Invincibility()
    {
        GameManager.instance.invincible = true;
        yield return new WaitForSecondsRealtime(7f);
        GameManager.instance.invincible = false;
    }

}
