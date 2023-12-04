using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HRLisa : MonoBehaviour
{
    public DialogueTrigger trigger;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger dialogue on collision with the player
            if (Input.GetKeyDown(KeyCode.J))
            {
                trigger.StartDialogue();
            }
        }
    }
}