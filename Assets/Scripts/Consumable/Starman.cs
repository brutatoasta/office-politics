using System.Collections;
using UnityEngine;


public class Starman : ABCConsumable
{

    public Starman(int initCount, int initCost)
    {
        count = initCount;
        cost = initCost;
        LoadSprite("Consumables/SuperStar");
    }
    public override void ConsumeEffect()
    {
        GameManager.instance.StartCoroutine(Invincibility());
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.Starman);
    }

    IEnumerator Invincibility()
    {
        GameManager.instance.invincible = true;
        yield return new WaitForSecondsRealtime(7f);
        GameManager.instance.invincible = false;
    }

}
