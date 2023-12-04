using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Shop to buy consumables
public class Shop : BaseInteractable
{
    public GameObject shopUI;
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
        shopUI.SetActive(true);
        GameManager.instance.playerFreeze.Invoke();

        // natthan - sfx for opening shop menu
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.purchaseItem);
    }
}

