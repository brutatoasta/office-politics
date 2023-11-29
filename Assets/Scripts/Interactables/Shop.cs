using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Shop to buy consumables
public class Shop : BaseInteractable
{
    public override void Start()
    {
        base.Start();
        
    }
    protected override bool CanInteract()
    {
        return true;
    }

    protected override void OnInteract()
    {
        // called when player presses interact key
        // GameManager.instance.runVariables.consumableObjects[(int) consumableType].count += 1;
        // GameManager.instance.runVariables.performancePoints -= GameManager.instance.runVariables.consumableCosts[(int) consumableType];
    }
    

}

