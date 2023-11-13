using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class Receivable : BaseInteractable
{
    public InventoryVariable inventory;
    public InteractableType validInput;
    public new void OnInteract(SpriteRenderer heldSprite)
    {
        // called when player presses interact key
        if (GameManager.instance.held != null)
        {
            // maybe check other conditions
            // drop the item in!
            Debug.Log("Dropped something into me!");
            GameObject held = GameManager.instance.held;
            GameManager.instance.held = null;
            heldSprite.sprite = null;
            // calculate score
            if (validInput != held.GetComponent<Holdable>().holdableType)
            {
                //decrease score
                inventory.stressPoint += 1;
                Debug.Log("decrease score");
            }
            else
            {
                //increase score
                inventory.stressPoint = (inventory.stressPoint>10)? inventory.stressPoint-10: 0; 
                inventory.performancePoint += 1;
                Debug.Log("increase score");
            }

        }

    }
}
