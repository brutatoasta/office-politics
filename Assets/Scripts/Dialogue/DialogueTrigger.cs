using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    public int[] messagePauses;
    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors, messagePauses);
    }
}
[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
    public UnityEvent triggerNextEvent;
}
[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}