using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class TaskGenerator : MonoBehaviour
{
    // Start is called before the first frame update
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

    void Start()
    {
        taskDescription.GetComponent<TextMeshProUGUI>().text = "";
        InitializeDescription();
    }

    // Update is called once per frame
    void Update()
    {
        InitializeDescription();
    }
    void InitializeDescription()
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
        // taskDescription.GetComponent<TextMeshProUGUI>().text;
    }
    private string GenerateTaskString(string taskName, int taskCount)
    {
        if (taskCount > 0)
        {
            return $"{taskName} x{taskCount}\n";
        }
        return "";
    }
}
