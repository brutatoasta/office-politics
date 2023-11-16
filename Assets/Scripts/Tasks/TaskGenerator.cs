using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class TaskGenerator : MonoBehaviour
{
    // creates strings for each task
    public GameObject taskDescription;
    public TaskConstants taskConstants;
    StringBuilder stringBuilder = new StringBuilder();
    string fetchCoffeeTaskString;
    string refillCoffeeTaskString;
    string fetchTeaTaskString;
    string fetchDocumentTaskString;
    string deliverDocumentTaskString;
    string chargeMicrophoneTaskString;
    string prepMeetingMaterialsTaskString;
    string refereshmentsTaskString;
    string collectDocumentsTaskString;
    string shredDocumentsTaskString;
    string laminateDocumentTaskString;
    string returnDocumentTaskString;

    int initialize;

    void Start()
    {
        taskDescription.GetComponent<TextMeshProUGUI>().text = "";
        generateDescription();
        InitializeConstants();
        initialize = 0;
    }

    void Update()
    {
        UpdateDescription();
    }
    void generateDescription()
    {
        fetchCoffeeTaskString = GenerateTaskString("Fetch Coffee for Anna", taskConstants.fetchCoffeeTask);
        refillCoffeeTaskString = GenerateTaskString("Refill Coffee in the pantry", taskConstants.refillCoffeeTask);
        fetchTeaTaskString = GenerateTaskString("Fetch tea for Mark", taskConstants.fetchTeaTask);
        fetchDocumentTaskString = GenerateTaskString("Fetch photocopy document from Emma", taskConstants.fetchDocumentTask);
        deliverDocumentTaskString = GenerateTaskString("Deliver photocopy document to Alex", taskConstants.deliverDocumentTask);
        chargeMicrophoneTaskString = GenerateTaskString("Charge Microphone in the meeting room before 3pm", taskConstants.chargeMicrophoneTask);
        prepMeetingMaterialsTaskString = GenerateTaskString("Prepare meeting room before 3pm", taskConstants.prepMeetingMaterialsTask);
        refereshmentsTaskString = GenerateTaskString("Send refreshment to meeting room", taskConstants.refereshmentsTask);
        collectDocumentsTaskString = GenerateTaskString("Collect document from BOSS", taskConstants.collectDocumentsTask);
        shredDocumentsTaskString = GenerateTaskString("Shred documents", taskConstants.shredDocumentsTask);
        laminateDocumentTaskString = GenerateTaskString("Laminate documents", taskConstants.laminateDocumentTask);
        returnDocumentTaskString = GenerateTaskString("Return document to BOSS", taskConstants.returnDocumentTask);

        // stringBuilder.Append(fetchCoffeeTaskString);
        // stringBuilder.Append(refillCoffeeTaskString);
        // stringBuilder.Append(fetchTeaTaskString);
        // stringBuilder.Append(fetchDocumentTaskString);
        // stringBuilder.Append(deliverDocumentTaskString);
        // stringBuilder.Append(chargeMicrophoneTaskString);
        // stringBuilder.Append(prepMeetingMaterialsTaskString);
        // stringBuilder.Append(refereshmentsTaskString);
        // stringBuilder.Append(collectDocumentsTaskString);
        // stringBuilder.Append(shredDocumentsTaskString);
        // stringBuilder.Append(laminateDocumentTaskString);
        // stringBuilder.Append(returnDocumentTaskString);
        taskDescription.GetComponent<TextMeshProUGUI>().text = fetchCoffeeTaskString + refillCoffeeTaskString + fetchTeaTaskString +
        fetchDocumentTaskString + deliverDocumentTaskString + chargeMicrophoneTaskString + prepMeetingMaterialsTaskString +
        refereshmentsTaskString + collectDocumentsTaskString + shredDocumentsTaskString + laminateDocumentTaskString + returnDocumentTaskString;

    }
    void UpdateDescription()
    {
        generateDescription();
    }
    void InitializeConstants()
    {
        taskConstants.fetchCoffeeTask = 1;
        taskConstants.refillCoffeeTask = 1;
        taskConstants.fetchTeaTask = 1;
        taskConstants.fetchDocumentTask = 1;
        taskConstants.deliverDocumentTask = 1;
        taskConstants.chargeMicrophoneTask = 1;
        taskConstants.prepMeetingMaterialsTask = 1;
        taskConstants.refereshmentsTask = 1;
        taskConstants.collectDocumentsTask = 1;
        taskConstants.shredDocumentsTask = 1;
        taskConstants.laminateDocumentTask = 1;
        taskConstants.returnDocumentTask = 1;
    }
    private string GenerateTaskString(string taskName, int taskCount)
    {
        if (taskCount > 0)
        {
            return $"{taskName} x{taskCount}\n";
        }
        // else if (initialize == 0)
        // {
        //     return "";
        // }
        return $"<s>{taskName} </s>\n";

    }
    private string StrikeOut(string textToStrike)
    {
        const string STRIKE_START = "<s>";
        const string STRIKE_END = "</s>";
        return STRIKE_START + textToStrike + STRIKE_END;
    }



}
