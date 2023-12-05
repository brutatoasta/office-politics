using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        _validInputs = new HashSet<TaskName>(validInputs);
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
        if (held == null)
        {
            if (isDocumentReady)
            {
                GameManager.instance.SetHeld(deliverDoc);
                playerHand.sprite = deliverDocSprite;
                Debug.Log($"Fetched {held.name} from printer!");
                isDocumentReady = false;
                progressBar.SetActive(false);

                if (heldType == TaskName.DeliverDoc)
                {
                    // natthan - sfx for pick up document from printer
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.showTaskDetails);
                }
                else if (heldType == TaskName.RefillCoffee)
                {
                    // natthan - sfx for pick up refilled coffee pot
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveRefreshment);
                }
            }

        }
        else
        {
            if (IsValidInput(heldType)) // not just holdable class, but specfically accept toShred and toLaminate types 
            {
                GameManager.instance.levelVariables.Succeed(heldType);
                GameManager.instance.showPerformancePoint.Invoke();
                GameManager.instance.DecreaseQuota();
                // depositing PrintDoc
                GameManager.instance.SetHeld(null);
                playerHand.sprite = null;
                playerHand.color = Color.white;

                progressBar.SetActive(true);
                progressBarAnimator.SetTrigger("photocopyProgress");

                if (heldType == TaskName.FetchDoc)
                {
                    // natthan - sfx for photocopy
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.photocopyDocument);
                }
                else if (heldType == TaskName.emptyCoffeePot)
                {
                    // natthan temporary - sfx for refill coffee pot
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.refillCoffeePot);
                }

                // cook the doc
                StartCoroutine(Cook());
            }
        }
    }
    bool IsValidInput(TaskName input)
    {
        return _validInputs.Contains(input);
    }
    private IEnumerator Cook()
    {
        // coroutine changes isDocumentReady from false to true after x seconds
        //Debug.Log($"Waiting for {held.name} to finish processing");
        yield return new WaitForSeconds(cookTime);
        isDocumentReady = true;
    }
}
