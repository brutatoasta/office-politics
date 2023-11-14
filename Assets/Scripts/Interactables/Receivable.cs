using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class Receivable : BaseInteractable
{
    public InteractableType validInput;
    public PlayerConstants playerConstants;
    public TaskConstants taskConstants;


    public new void OnInteract(SpriteRenderer heldSprite)
    {
        // called when player presses interact key
        taskConstants.currentInput = validInput;
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
                playerConstants.performancePoint -= 5;
                Debug.Log("decrease score");
            }
            else
            {
                playerConstants.performancePoint += 5;
                Debug.Log("increase score");
                if (validInput == InteractableType.ToPrepMeeting)
                {
                    GameManager.instance.showMeetingDocs.Invoke();
                }
                GameManager.instance.switchTasks.Invoke();
            }

        }

    }
}
