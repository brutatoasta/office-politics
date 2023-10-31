using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class Interactable : MonoBehaviour, IInteractables
{
    public InteractableType type = InteractableType.Interactable;
    public void OnInteract()
    {
        // called when player presses interact key
        Debug.Log("Interacted with me!");
    }

    private SpriteRenderer map;

    void Start()
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

}
