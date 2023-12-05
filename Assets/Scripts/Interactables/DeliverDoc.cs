using UnityEngine;


// Takes and handles input and movement for a player character
public class DeliverDoc : BaseInteractable
{
    private GameObject held;
    private Holdable holdable;
    [SerializeField]
    private GameObject fetchDoc; // placed outside map
    private Sprite fetchDocSprite;
    new void Awake()
    {
        base.Awake();
        fetchDocSprite = fetchDoc.GetComponent<SpriteRenderer>().sprite;
    }
    protected override void OnInteract()
    {
        // called when player presses interact key


        // if empty hand, put object into hand
        if (GameManager.instance.held == null)
        {
            // add self to GameManager
            GameManager.instance.SetHeld(fetchDoc);
            playerHand.sprite = fetchDocSprite;

            // sfx for picking up photocopy document
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.pickUpDocument);
        }
        else
        {
            // empty player's hand of deliverDoc
            GameManager.instance.SetHeld(null);
            playerHand.sprite = null;
            playerHand.color = Color.white;
            GameManager.instance.levelVariables.Succeed(TaskName.DeliverDoc);

            // sfx for placing photocopy document
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.hideTaskDetails);
        }
    }

    protected override bool CanInteract()
    {   // called when player walks into interact hitbox
        held = GameManager.instance.held;
        if (held == null)
        {
            // empty hand so we can pick up DeliverDoc
            // TODO: check quota?
            return true;
        }
        else
        {
            holdable = held.GetComponent<Holdable>();
            if (holdable)
            {
                // check that what we're holding is a DeliverDoc
                return holdable.taskName == TaskName.DeliverDoc;
            }
            else
            {
                return false;
            }
        }
    }
}
