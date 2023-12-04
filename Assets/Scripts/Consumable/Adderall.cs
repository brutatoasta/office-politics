using System;
using System.Collections;
using UnityEngine;


public class Adderall : ABCConsumable
{
    public float oldCooldownParry;
    public float oldCooldownDash;
    public Adderall(int initCount, int initCost)
    {
        count = initCount;
        cost = initCost;
        LoadSprite("Consumables/Pills"); // TODO: replace sprite
        oldCooldownParry = GameManager.instance.playerConstants.parryCooldown;
        oldCooldownDash = GameManager.instance.playerConstants.dashCooldown;
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.Coffee);
    }
    public override void ConsumeEffect()
    {
        GameManager.instance.StartCoroutine(TempLowerCooldown());
    }
    IEnumerator TempLowerCooldown()
    {
        GameManager.instance.playerConstants.parryCooldown = 0.3f;
        GameManager.instance.playerConstants.dashCooldown = 0.1f;
        GameManager.instance.cooldownPercent = GameManager.instance.levelVariables.evadeType == EvadeType.Dash? 0.1f/oldCooldownDash: 0.3f/oldCooldownParry;
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.Adderall);
        yield return new WaitForSecondsRealtime(10);
        GameManager.instance.cooldownPercent = 1f;
        GameManager.instance.consumableEfffect.Invoke(ConsumableType.Adderall);
        GameManager.instance.playerConstants.parryCooldown = oldCooldownParry;
        GameManager.instance.playerConstants.dashCooldown = oldCooldownDash;
    }
}
