using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Represents an interactable object that can receive items from players.
// Shredder and Laminator
public class Receivable : BaseInteractable
{
    public InteractableType[] validInputs; // can change when tasks get added
    private HashSet<InteractableType> _validInputs; // hashset for faster checks

    new void Awake()
    {
        base.Awake();
        _validInputs = new HashSet<InteractableType>(validInputs); // TODO: no need multiple inputs
    }
    protected override bool CanInteract()
    {
        GameObject held = GameManager.instance.held;

        if (held == null) // nothing in hand
        {
            return false;
        }
        else
        {
            //something in hand
            // check what it is
            // if its holdable of appropriate type, return true
            // no other scripts or interactables can be held anyway?
            // TODO
            return isValidInput(held.GetComponent<Holdable>().holdableType);
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
            GameObject held = GameManager.instance.held;
            GameManager.instance.held = null;
            playerHand.sprite = null;

            Debug.Log($"Dropped {held.name} into me!");

            // calculate score and tasks
            InteractableType heldType = held.GetComponent<Holdable>().holdableType;
            if (isValidInput(heldType)) // not just holdable class, but specfically accept toShred and toLaminate types 
            {
                // decrease task count
                GameManager.instance.levelVariables.Succeed((TaskName)heldType);
                Debug.Log("increase score");

                // TODO: if machine and not a person
                animator.SetTrigger("doWiggle");
                // putting papers/refreshment has no fail condition, but maybe put this in another method/switch statement based on validInputs
                if (anyAreValidInput(new[] { InteractableType.ToPrepMeeting, InteractableType.ToPrepRefreshment }))
                {
                    sprite.enabled = true;
                }
            }
            else
            {
                // decrease score
                GameManager.instance.levelVariables.Fail((TaskName)heldType);
                Debug.Log("decrease score");
                animator.SetTrigger("doFlinch");
            }

        }
    }

    bool isValidInput(InteractableType input)
    {
        return _validInputs.Contains(input);
    }

    bool allAreValidInput(InteractableType[] inputs)
    {
        foreach (InteractableType value in inputs)
        {
            if (!_validInputs.Contains(value))
            {
                return false;
            }

        }
        return true;
    }
    bool anyAreValidInput(InteractableType[] inputs)
    {
        foreach (InteractableType value in inputs)
        {
            if (_validInputs.Contains(value))
            {
                return true;
            }
        }
        return false;
    }
}

