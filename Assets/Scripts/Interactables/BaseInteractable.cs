using System;
using System.Threading.Tasks;
using UnityEngine;


// Takes and handles input and movement for a player character
public abstract class BaseInteractable : MonoBehaviour
{
    // public TaskName iType = TaskName.Default; // my own type 

    protected Animator animator;
    [System.NonSerialized] public SpriteRenderer playerHand;
    [System.NonSerialized] public Renderer spriteRenderer;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<Renderer>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 7) //player
        {
            playerHand = col.transform.GetChild(0).GetComponent<SpriteRenderer>();
            // if player can interact, light up
            if (CanInteract())
            {
                // TODO: turn on shader
                spriteRenderer.material = GameManager.instance.taskConstants.highlightMaterial;
                

                // subscribe to gamemanager's interact event
                GameManager.instance.interact.AddListener(OnInteract);

            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 7)
        {
            // TODO: turn off shader
            spriteRenderer.material = GameManager.instance.taskConstants.defaultMaterial;

            // unsubscribe to gamemanager's interact event
            GameManager.instance.interact.RemoveListener(OnInteract);
        }
    }
    // checks if player is allowed to interact with this object.
    // check player's hand
    // if held item can interact with self, return true
    // if empty item and can interact with self, return true
    // else return false
    // uses the subclass 
    protected abstract bool CanInteract();
    protected abstract void OnInteract();

}
