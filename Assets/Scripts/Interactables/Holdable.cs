using UnityEngine;


// Takes and handles input and movement for a player character
public class Holdable : BaseInteractable
{
    public TaskName taskName;
    public Sprite optionalSprite;
    protected override void OnInteract()
    {
        // called when player presses interact key


        // if empty hand, put object into hand
        if (GameManager.instance.held == null)
        {
            Debug.Log("Held me!");
            // add self to GameManager
            GameManager.instance.SetHeld(gameObject);
            playerHand.sprite = optionalSprite ?? GetComponent<SpriteRenderer>().sprite;
            playerHand.color = GetComponent<SpriteRenderer>().color;

            // check which task and play the sfx accordingly - Natthan
            switch (taskName)
            {
                case TaskName.FetchCoffee:
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.pourCoffee);
                    break;
                case TaskName.RefillCoffee:
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveCoffee);
                    break;
                case TaskName.FetchTea:
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.pourCoffee);
                    break;
                case TaskName.PrepRefreshment:
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.serveCoffee);
                    break;
                // natthan temporary - sound for picking up meeting documents
                case TaskName.PrepMeeting:
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.showTaskDetails);
                    break;
            }
        }
    }

    protected override bool CanInteract()
    {
        return GameManager.instance.held == null;

    }
}
