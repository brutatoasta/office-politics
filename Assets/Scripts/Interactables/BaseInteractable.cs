using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class BaseInteractable : MonoBehaviour, IInteractables
{
    public InteractableType type;
    private SpriteRenderer map;

    // highlighting here
    protected void Start()
    {
        map = GetComponent<SpriteRenderer>();
        map.color = Color.gray;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("Near me!");
        // triggers every frame
        if (col.gameObject.layer == 7)
        {
            map.color = Color.white;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 7)
        {
            map.color = Color.gray;
        }
    }

    public void OnInteract(SpriteRenderer heldSprite)
    {// called when player presses interact key
        switch (type)
        {
            case InteractableType.Holdable:
                // if empty hand, put object into hand
                if (!GameManager.instance.held)
                {
                    Debug.Log("Held me!");
                    // add self to GameManager
                    GameManager.instance.held = gameObject;
                    Debug.Log(GetComponent<SpriteRenderer>().sprite);
                    heldSprite.sprite = GetComponent<SpriteRenderer>().sprite;

                }
                break;
            case InteractableType.Interactable:
                Debug.Log("Interacted with me!");
                break;
            case InteractableType.Receivable:
                if (GameManager.instance.held != null)
                {
                    // maybe check other conditions
                    // drop the item in!
                    Debug.Log("Dropped something into me!");
                    GameManager.instance.held = null;
                    heldSprite.sprite = null;
                }
                break;
        }



    }
}
