using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HRLisa : MonoBehaviour
{
    public DialogueTrigger trigger;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) //player
        {
            GameManager.instance.interact.AddListener(OnInteract);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) //player
        {
            GameManager.instance.interact.RemoveListener(OnInteract);
        }
    }

    private void OnInteract()
    {
        // Trigger dialogue on collision with the player
        if (Input.GetKeyDown(KeyCode.J))
        {
            trigger.StartDialogue();
        }
    }
}