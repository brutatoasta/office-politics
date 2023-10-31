using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class Holdable : Interactable
{
    public new InteractableType type = InteractableType.Holdable;
    public void OnInteract(SpriteRenderer heldSprite)
    {
        Debug.Log("Held me!");
        // add self to GameManager
        GameManager.instance.held = gameObject;
        heldSprite.sprite = GetComponent<SpriteRenderer>().sprite;
    }


}
