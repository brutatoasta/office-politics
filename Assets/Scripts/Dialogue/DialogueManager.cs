using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;
    Message[] currentMessages;
    Actor[] currentActors;
    int[] currentMessagePauses;
    int activeMessage = 0;
    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors, int[] messagePauses)
    {
        currentMessages = messages;
        currentActors = actors;
        currentMessagePauses = messagePauses;
        isActive = true;
        GameManager.instance.playerFreeze.Invoke();
        Debug.Log("Started conversation! Loaded messages: " + messages.Length);
        DisplayMessage();
        //animate open backgroundbox
        backgroundBox.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
    }
    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        currentMessages[activeMessage].triggerNextEvent.Invoke();

        AnimateTextColour();
    }
    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length && !currentMessagePauses.Contains<int>(activeMessage))
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation Ended!");
            //animate closing backgroundbox
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;
            if (activeMessage == currentMessages.Length)
            {
                activeMessage = 0;
            }
            GameManager.instance.playerUnFreeze.Invoke();
        }
    }
    void AnimateTextColour()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }
    void Update()
    {
        if (Input.GetKeyDown("j") && isActive == true)
        {
            NextMessage();
        }
    }
}