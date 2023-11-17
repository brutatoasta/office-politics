using System;
using UnityEngine;


// Takes and handles input and movement for a player character
public class BaseInteractable : MonoBehaviour
{
    public InteractableType iType; // my own type 
    public Type type;
    protected Animator animator;
    protected bool isTouching = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 7) //player
        {
            isTouching = true;

            // if player can interact, light up
            if (CanInteract())
            {
                // if GameManager.instance.interacting == null
                // add
                // turn on shader
                // subscribe to gamemanager's interact event
                
                GameManager.instance.interact.AddListener(OnInteract);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 7)
        {
            isTouching = false;
            // turn off shader  // unsubscribe to gamemanager's interact event
            GameManager.instance.interact.RemoveListener(OnInteract);
        }
    }

    protected virtual bool CanInteract()
    {
        // checks if player is allowed to interact with this object.
        // TODO: check player's hand
        // if held item can interact with self, return true
        // if empty item and can interact with self, return true
        // else return false
        // uses the subclass 

        return true;
    }

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        type = GetType();

    }
    protected virtual void OnInteract()
    {
        // called when player presses interact key
        Debug.Log("Interacted with me!");
        animator.SetTrigger("doWiggle");
    }

}
