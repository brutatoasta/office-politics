using System.Collections.Generic;
using UnityEngine;

// Represents an interactable object that can receive items from players.
// Shredder and Laminator
public class Receivable : BaseInteractable
{
    // shredder can accept both valid and invalid inputs
    public TaskName[] invalidInputs;
    public TaskName[] validInputs;
    public SpriteRenderer sprite;
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
        SpriteRenderer sprite = GetComponent<SpriteRenderer>(); // TODO: what for?


        if (GameManager.instance.held != null)
        {
            // maybe check other conditions
            // drop the item in!
            GameManager.instance.SetHeld(null);
            playerHand.sprite = null;
            playerHand.color = Color.white;

            //Debug.Log($"Dropped {held.name} into me!");

            // calculate score and tasks
            if (isValidInput(heldType)) // not just holdable class, but specfically accept toShred and toLaminate types 
            {
                // natthan - sfx for placing valid item
                switch (heldType)
                {
                    case TaskName.FetchCoffee:
                        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveRefreshment);
                        break;
                    case TaskName.RefillCoffee:
                        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveRefreshment);
                        break;
                    case TaskName.FetchTea:
                        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveRefreshment);
                        break;
                    case TaskName.PrepRefreshment:
                        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveRefreshment);
                        break;
                    case TaskName.Shred:
                        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.shredDocument);
                        break;
                    case TaskName.Laminate:
                        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.laminateDocument);
                        break;
                    default:
                        break;
                }

                // decrease task count
                GameManager.instance.levelVariables.Succeed(heldType);
                GameManager.instance.showPerformancePoint.Invoke();
                GameManager.instance.DecreaseQuota();
                if (sprite != null)
                {
                    sprite.enabled = true;
                }
                Debug.Log("increase score");
                // TODO: if machine and not a person
                if (animator != null)
                {
                    animator.SetTrigger("doWiggle");
                }
                // putting papers/refreshment has no fail condition, but maybe put this in another method/switch statement based on validInputs
                if (anyAreValidInput(new[] { TaskName.PrepMeeting, TaskName.PrepRefreshment }))
                {
                    sprite.enabled = true;
                }
            }
            else
            {
                // decrease score
                GameManager.instance.levelVariables.Fail(heldType);
                GameManager.instance.showPerformancePoint.Invoke();
                Debug.Log("decrease score");
                if (animator != null)
                {
                    animator.SetTrigger("doFlinch");
                }

                // natthan - sfx for placing invalid item
                switch (heldType)
                {
                    case TaskName.Shred:
                        if (gameObject.name == "Laminate Machine")
                        {
                            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.jamMachine);
                        }
                        break;
                    case TaskName.Laminate:
                        if (gameObject.name == "Shred Machine")
                        {
                            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.jamMachine);
                        }
                        break;
                    default:
                        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.interactionInvalid);
                        break;
                }


            }

        }
    }

    bool isValidInput(TaskName input)
    {
        return _validInputs.Contains(input);
    }

    bool allAreValidInput(TaskName[] inputs)
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
    bool anyAreValidInput(TaskName[] inputs)
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

