using System;
using System.Collections;
using UnityEngine;


public class Adderall : ABCConsumable
{
    private float oldCooldownParry;
    private float oldCooldownDash;
    public Adderall(int initCount)
    {
        count = initCount;
        LoadSprite("Inventory"); // TODO: replace sprite
        oldCooldownParry = GameManager.instance.playerConstants.parryCooldown;
        oldCooldownDash = GameManager.instance.playerConstants.dashCooldown;
    }
    public override void ConsumeEffect()
    {
        GameManager.instance.StartCoroutine(TempLowerCooldown());
    }
    IEnumerator TempLowerCooldown()
    {
        
        GameManager.instance.playerConstants.parryCooldown = 0.2f;
        GameManager.instance.playerConstants.dashCooldown = 0.2f;
        yield return new WaitForSecondsRealtime(10);
        GameManager.instance.playerConstants.parryCooldown = oldCooldownParry;
        GameManager.instance.playerConstants.dashCooldown = oldCooldownDash;
    }
}
