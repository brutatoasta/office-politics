using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class Printer : BaseInteractable
{
    public TaskName[] invalidInputs;
    public TaskName[] validInputs;
    private HashSet<TaskName> _invalidInputs;
    private HashSet<TaskName> _validInputs; // hashset for faster checks
    TaskName heldType;
    GameObject held;

    // FetchDoc
    private bool isDocumentReady = false;
    private GameObject fetchDoc; // placed outside map
    private Sprite fetchDocSprite;
    private float cookTime;
    new void Awake()
    {
        base.Awake();

        _validInputs = new HashSet<TaskName>(validInputs); // TODO: no need multiple inputs
        _invalidInputs = new HashSet<TaskName>(invalidInputs);
        fetchDocSprite = fetchDoc.GetComponent<SpriteRenderer>().sprite;
    }
    protected override bool CanInteract()
    {
        // you can only interact with printer when your hand has a PrintDoc or your hand is empty 
        // but the printer has a to fetch doc
        held = GameManager.instance.held;
        if (held == null) // nothing in hand
        {
            return isDocumentReady;
        }
        else
        {
            heldType = held.GetComponent<Holdable>().taskName;
            return _validInputs.Contains(heldType);
        }

    }

    protected override void OnInteract()
    {
        // fetching doc
        if (GameManager.instance.held == null)
        {

            GameManager.instance.held = fetchDoc;
            playerHand.sprite = fetchDocSprite;
            Debug.Log($"Fetched {held.name} from printer!");
            isDocumentReady = false;
        }
        else
        {
            // depositing PrintDoc
            GameManager.instance.held = null;
            playerHand.sprite = null;

            Debug.Log($"Dropped {held.name} into printer!");
            animator.SetTrigger("doWiggle");

            // cook the doc
            Cook();
        }
    }
    private void Cook()
    {
        // coroutine changes isDocumentReady from false to true after x seconds
    }
}
