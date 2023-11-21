using System;
using System.Threading.Tasks;
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
            Debug.Log($"Fetched {fetchDoc.name} from colleague!");
            // add self to GameManager
            GameManager.instance.held = fetchDoc;
            playerHand.sprite = fetchDocSprite;
        }
        else
        {
            // empty player's hand of deliverDoc
            Debug.Log($"Dropped {held.name} into colleague!");
            GameManager.instance.held = null;
            playerHand.sprite = null;
            GameManager.instance.levelVariables.Succeed(TaskName.DeliverDoc);
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
