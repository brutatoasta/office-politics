using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Shop to buy consumables
public class Shop : BaseInteractable
{
    public ConsumableType consumableType;
    new void Awake()
    {
        base.Awake();
    }
    protected override bool CanInteract()
    {
        RunVariables state = GameManager.instance.runVariables;
        return state.performancePoints >= state.consumableCosts[(int) consumableType];
    }

    protected override void OnInteract()
    {
        // called when player presses interact key
        GameManager.instance.runVariables.consumableObjects[(int) consumableType].count += 1;
        GameManager.instance.runVariables.performancePoints -= GameManager.instance.runVariables.consumableCosts[(int) consumableType];
    }
    IEnumerator AnimateInteraction()
    {
        GameManager.instance.SetHeld(gameObject);
        yield return new WaitForSecondsRealtime(0.1f);
        GameManager.instance.SetHeld(null);
    }

}

