using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class CoffeePot : BaseInteractable
{
    public TaskName taskName;
    private Holdable holdable;

    public SpriteRenderer sprite;
    private GameObject held;
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
        else
        {
            // empty player's hand of deliverDoc
            sprite.enabled = false;
            GameManager.instance.SetHeld(null);
            playerHand.sprite = null;
            playerHand.color = Color.white;
            GameManager.instance.levelVariables.Succeed(TaskName.FetchCoffee);

            // Natthan

        }


    }

    protected override bool CanInteract()
    {
        held = GameManager.instance.held;
        if (held == null)
        {
            return true;
        }
        else
        {
            holdable = held.GetComponent<Holdable>();
            if (holdable)
            {
                return holdable.taskName == TaskName.FetchCoffee;
            }
            else
            {
                return false;
            }
        }
    }
}
