using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Represents an interactable object that can receive items from players.
// Shredder and Laminator
public class Trashbin : BaseInteractable
{
    // shredder can accept both valid and invalid inputs
    public TaskName[] invalidInputs;
    public TaskName[] validInputs;
    public GameObject taskIcon;
    private HashSet<TaskName> _invalidInputs;
    private HashSet<TaskName> _validInputs; // hashset for faster checks
    TaskName heldType;
    GameObject held;

    new void Awake()
    {
        base.Awake();

        _validInputs = new HashSet<TaskName>(validInputs); // TODO: no need multiple inputs
        _invalidInputs = new HashSet<TaskName>(invalidInputs);
        if (taskIcon != null)
        {
            taskIcon.SetActive(true);
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

            Debug.Log($"Dropped {held.name} into me!");

            // Natthan
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.throwTrash);
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

