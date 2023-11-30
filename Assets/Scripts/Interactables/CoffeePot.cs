using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class CoffeePot : BaseInteractable
{
    public TaskName taskName;

    public SpriteRenderer sprite;
    protected override void OnInteract()
    {
        // called when player presses interact key
        // if empty hand, put object into hand
        if (GameManager.instance.held == null)
        {
            Debug.Log("Held me!");
            // add self to GameManager
            GameManager.instance.SetHeld(gameObject);
            playerHand.sprite = GetComponent<SpriteRenderer>().sprite;
            playerHand.color = GetComponent<SpriteRenderer>().color;
            sprite.enabled = true;

            // temporary - Natthan
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveCoffee);
        }

    }

    protected override bool CanInteract()
    {
        return GameManager.instance.held == null;
    }
}
