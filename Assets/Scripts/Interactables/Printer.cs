using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class Printer : Receivable
{
    public bool isScanning = false;
    // public new void OnInteract(SpriteRenderer heldSprite)
    // {
    //     // called when player presses interact key
    //     taskConstants.currentInput = validInput; // TODO: why is this line co opting what we put into the receivable via the editor?
    //     SpriteRenderer sprite = GetComponent<SpriteRenderer>();
    //     if (GameManager.instance.held != null)
    //     {
    //         // maybe check other conditions
    //         // drop the item in!
    //         GameObject held = GameManager.instance.held;
    //         GameManager.instance.held = null;
    //         heldSprite.sprite = null;

    //         Debug.Log($"Dropped {held.name} into me!");

    //         // calculate score
    //         if (validInput != held.GetComponent<Holdable>().holdableType)
    //         {
    //             // decrease score
    //             playerConstants.performancePoint -= 5;
    //             Debug.Log("decrease score");
    //             animator.SetTrigger("doFlinch");
    //         }
    //         else
    //         {
    //             playerConstants.performancePoint += 5;
    //             Debug.Log("increase score");
    //             // animate for some time
    //             animator.SetBool("isWiggling", true);

    //             GameManager.instance.switchTasks.Invoke();
    //         }

    //     }

    // }
}
