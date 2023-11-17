using System;
using UnityEngine;


// Takes and handles input and movement for a player character
public abstract class BaseInteractable : MonoBehaviour
{
    public InteractableType iType; // my own type 

    protected Animator animator;
    public SpriteRenderer playerHand;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 7) //player
        {
            playerHand = col.transform.GetChild(0).GetComponent<SpriteRenderer>();
            Debug.LogError("in Hand");
            // if player can interact, light up
            if (CanInteract())
            {
                // if GameManager.instance.interacting == null
                // add
                // turn on shader
                // subscribe to gamemanager's interact event

                GameManager.instance.interact.AddListener(OnInteract);
                Debug.LogError("addedlistener");
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 7)
        {
            // turn off shader  // unsubscribe to gamemanager's interact event
            Debug.LogError("remove listener");
            GameManager.instance.interact.RemoveListener(OnInteract);
        }
    }
    // checks if player is allowed to interact with this object.
    // TODO: check player's hand
    // if held item can interact with self, return true
    // if empty item and can interact with self, return true
    // else return false
    // uses the subclass 
    protected abstract bool CanInteract();
    protected abstract void OnInteract();

}
