using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class CoffeePot : BaseInteractable
{
    //public TaskName taskName;
    public SpriteRenderer sprite;
    private GameObject held;
    private Holdable holdable;
    [SerializeField]
    private GameObject refillCoffee;

    private Sprite refillCoffeeSprite;


    new void Awake()
    {
        base.Awake();
        refillCoffeeSprite = refillCoffee.GetComponent<SpriteRenderer>().sprite;
    }
    protected override void OnInteract()
    {
        // called when player presses interact key
        // if empty hand, put object into hand
        if (GameManager.instance.held == null)
        {
            Debug.Log("Held me!");
            // add self to GameManager
            GameManager.instance.SetHeld(refillCoffee);
            playerHand.sprite = refillCoffeeSprite;
            sprite.enabled = true;

            // natthan - sfx for picking up coffee pot
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveRefreshment);
        }
        else
        {
            // empty player's hand of deliverDoc
            sprite.enabled = false;
            GameManager.instance.SetHeld(null);
            playerHand.sprite = null;
            GameManager.instance.levelVariables.Succeed(TaskName.RefillCoffee);

            // natthan - sfx for placing coffee pot
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveRefreshment);
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
                return holdable.taskName == TaskName.RefillCoffee;
            }
            else
            {
                return false;
            }
        }
    }
}
