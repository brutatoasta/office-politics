using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class BaseInteractable : InteractableHighlight, IInteractables
{
    public InteractableType type; // type is for different logic

    public void OnInteract(SpriteRenderer heldSprite)
    {
        // called when player presses interact key
        Debug.Log("Interacted with me!");
    }
    public bool CastAndInteract(SpriteRenderer heldSprite)
    {
        switch (type)
        {
            case InteractableType.Receivable:
                ((Receivable)this).OnInteract(heldSprite);
                // TODO check if player is holding object and allowed to deposit
                return true;
            case InteractableType.Holdable:
                ((Holdable)this).OnInteract(heldSprite);
                // TODO check if player is holding object and allowed to pickup another
                return true;
            default:
                OnInteract(heldSprite);
                return true;
        }
    }
}
