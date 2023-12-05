using System.Collections.Generic;
using UnityEngine;

// Represents an interactable object that can receive items from players.
// Colleague who receive 
public class ReceivableMeeting : BaseInteractable
{
    // shredder can accept both valid and invalid inputs
    public TaskName[] invalidInputs;
    public TaskName[] validInputs;

    public SpriteRenderer tableSprite;
    private HashSet<TaskName> _invalidInputs;
    private HashSet<TaskName> _validInputs; // hashset for faster checks
    TaskName heldType;
    GameObject held;

    new void Awake()
    {
        base.Awake();

        _validInputs = new HashSet<TaskName>(validInputs); // TODO: no need multiple inputs
        _invalidInputs = new HashSet<TaskName>(invalidInputs);
        if (bubbleObj != null)
        {
            bubbleObj.SetActive(true);
        }
        tableSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        tableSprite.enabled = false; // hide it first
    }
    protected override bool CanInteract()
    {
        held = GameManager.instance.held;

        if (held == null) // nothing in hand
        {
            return false;
        }
        else
        {
            heldType = held.GetComponent<Holdable>().taskName;
            return _validInputs.Contains(heldType) || _invalidInputs.Contains(heldType);
        }

    }

    protected override void OnInteract()
    {
        // called when player presses interact key

        if (GameManager.instance.held != null)
        {
            // drop the item in!
            GameManager.instance.SetHeld(null);
            playerHand.sprite = null;
            playerHand.color = Color.white;

            // calculate score and tasks
            if (IsValidInput(heldType)) // not just holdable class, but specfically accept toShred and toLaminate types 
            {
                // natthan - refreshment and prep meeting
                if (heldType == TaskName.PrepRefreshment)
                {
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveRefreshment);
                }
                else if (heldType == TaskName.PrepMeeting)
                {
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.hideTaskDetails);
                }

                // decrease task count
                GameManager.instance.levelVariables.Succeed(heldType);
                GameManager.instance.showPerformancePoint.Invoke();
                GameManager.instance.DecreaseQuota();

                // show sprite on table
                tableSprite.enabled = true;

                Debug.Log("increase score");

                // disable the bubble notif
                if (bubbleObj != null)
                {
                    bubbleObj.SetActive(false);
                }
            }
            else
            {
                // decrease score
                GameManager.instance.levelVariables.Fail(heldType);
                GameManager.instance.showPerformancePoint.Invoke();
                Debug.Log("decrease score");


                // Play "interactionInvalid" sound - Natthan
                // Todo: can add more specific sounds in the future
                GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.interactionInvalid);
            }

        }
    }

    bool IsValidInput(TaskName input)
    {
        return _validInputs.Contains(input);
    }

    bool AllAreValidInput(TaskName[] inputs)
    {
        foreach (TaskName value in inputs)
        {
            if (!_validInputs.Contains(value))
            {
                return false;
            }

        }
        return true;
    }

    private bool AnyAreValidInput(TaskName[] inputs)
    {
        foreach (TaskName value in inputs)
        {
            if (_validInputs.Contains(value))
            {
                return true;
            }
        }
        return false;
    }
}

