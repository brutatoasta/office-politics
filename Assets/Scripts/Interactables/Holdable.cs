using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class Holdable : BaseInteractable
{
    public TaskName taskName;
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

            // check which task and play the sfx accordingly - Natthan
            if (taskName == TaskName.FetchCoffee)
            {
                GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.pourCoffee);
            }
            else if (taskName == TaskName.RefillCoffee)
            {
                GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveCoffee);
            }
            else if (taskName == TaskName.FetchTea)
            {
                GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.pourCoffee);
            }
            else if (taskName == TaskName.PrepRefreshment)
            {
                GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveCoffee);
            }
        }
    }

    protected override bool CanInteract()
    {
        return GameManager.instance.held == null;

    }
}
