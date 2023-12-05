using System.Collections;
using UnityEngine;


public class Adderall : ABCConsumable
{
    public float oldCooldownParry;
    public float oldCooldownDash;

    // Constructor sets sprite and caches old cooldown values
    public Adderall(int initCount, int initCost)
    {
        count = initCount;
        cost = initCost;
        LoadSprite("Consumables/Pills");
        oldCooldownParry = GameManager.instance.playerConstants.parryCooldown;
        oldCooldownDash = GameManager.instance.playerConstants.dashCooldown;
    }

    // Implementation of abstract class for effect
    public override void ConsumeEffect()
    {
        GameManager.instance.StartCoroutine(TempLowerCooldown());
    }

    // Coroutine to lower cooldown
    IEnumerator TempLowerCooldown()
    {
        GameManager.instance.playerConstants.parryCooldown = 1f;
        GameManager.instance.playerConstants.dashCooldown = 1f;
        GameManager.instance.cooldownPercent = GameManager.instance.levelVariables.evadeType == EvadeType.Dash? 1f/oldCooldownDash: 1f/oldCooldownParry;
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.Adderall);
        yield return new WaitForSecondsRealtime(10);
        GameManager.instance.cooldownPercent = 1f;
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.Adderall);
        GameManager.instance.playerConstants.parryCooldown = oldCooldownParry;
        GameManager.instance.playerConstants.dashCooldown = oldCooldownDash;
    }
}
