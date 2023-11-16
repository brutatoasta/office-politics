using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class Receivable : BaseInteractable
{
    public InventoryVariable inventory;
    public InteractableType[] validInputs;
    private HashSet<InteractableType> _validInputs; // hashset for faster checks
    public PlayerConstants playerConstants;
    public TaskConstants taskConstants;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        _validInputs = new HashSet<InteractableType>(validInputs); // TODO no need multiple inputs
    }

    public new void OnInteract(SpriteRenderer heldSprite)
    {
        // called when player presses interact key
        taskConstants.currentInput = validInputs; // TODO why is this line co opting what we put into the receivable via the editor? // what is tasks?
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (GameManager.instance.held != null)
        {
            // maybe check other conditions
            // drop the item in!
            GameObject held = GameManager.instance.held;
            GameManager.instance.held = null;
            heldSprite.sprite = null;

            Debug.Log($"Dropped {held.name} into me!");

            // calculate score
            if (!isValidInput(held.GetComponent<Holdable>().holdableType))
            {
                // decrease score
                // playerConstants.performancePoint -= 5;
                GameManager.instance.increasePerformancePoint.Invoke(-5, transform);
                Debug.Log("decrease score");
                animator.SetTrigger("doFlinch");
            }
            else
            {
                GameManager.instance.increasePerformancePoint.Invoke(5, transform);
                Debug.Log("increase score");
                // TODO if machine and not a person
                animator.SetTrigger("doWiggle");
                // putting papers/refreshment has no fail condition, but maybe put this in another method/switch statement based on validInputs
                if (anyAreValidInput(new[] { InteractableType.ToPrepMeeting, InteractableType.ToPrepRefreshment }))
                {
                    sprite.enabled = true;
                }
                GameManager.instance.switchTasks.Invoke(); // TODO what is switch tasks
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

