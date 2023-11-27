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
    private TaskName heldType;
    private GameObject held;

    // DeliverDoc
    private bool isDocumentReady = false;
    [SerializeField]
    private GameObject deliverDoc; // placed outside map
    private Sprite deliverDocSprite;
    private float cookTime = 2;
    public GameObject progressBar;
    public Animator progressBarAnimator;
    new void Awake()
    {
        base.Awake();

        _validInputs = new HashSet<TaskName>(validInputs); // TODO: no need multiple inputs
        _invalidInputs = new HashSet<TaskName>(invalidInputs);
        deliverDocSprite = deliverDoc.GetComponent<SpriteRenderer>().sprite;
        progressBar.SetActive(false);
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

            GameManager.instance.SetHeld(deliverDoc);
            playerHand.sprite = deliverDocSprite;
            Debug.Log($"Fetched {GameManager.instance.held.name} from printer!");
            isDocumentReady = false;
            progressBar.SetActive(false);
        }
        else
        {
            // depositing PrintDoc
            GameManager.instance.SetHeld(null);
            playerHand.sprite = null;

            Debug.Log($"Dropped {held.name} into printer!");
            progressBar.SetActive(true);
            progressBarAnimator.SetTrigger("photocopyProgress");

            // cook the doc
            StartCoroutine(Cook());
        }
    }
    private IEnumerator Cook()
    {
        // coroutine changes isDocumentReady from false to true after x seconds
        Debug.Log($"Waiting for {held.name} to finish processing");
        yield return new WaitForSeconds(cookTime);
        isDocumentReady = true;
        Debug.Log("document ready");

    }
}
