using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    // Start is called before the first frame update
    public TaskConstants taskConstants;
    void Start()
    {
        GameManager.instance.switchTasks.AddListener(TaskManager);

    }
    public void TaskManager()
    {
        switch (taskConstants.currentInput)
        {
            case InteractableType.ToShred:
                if (taskConstants.shredDocumentsTask > 0)
                {
                    taskConstants.shredDocumentsTask--;
                }
                break;
            case InteractableType.ToLaminate:
                if (taskConstants.laminateDocumentTask > 0)
                {
                    taskConstants.laminateDocumentTask--;
                }
                break;
            case InteractableType.ToFetchCoffee:
                if (taskConstants.fetchCoffeeTask > 0)
                {
                    taskConstants.fetchCoffeeTask--;
                }
                break;
            case InteractableType.ToRefillCoffee:
                if (taskConstants.refillCoffeeTask > 0)
                {
                    taskConstants.refillCoffeeTask--;
                }
                break;
            case InteractableType.ToFetchTea:
                if (taskConstants.fetchTeaTask > 0)
                {
                    taskConstants.fetchTeaTask--;
                }
                break;
            case InteractableType.ToFetchDoc:
                if (taskConstants.fetchDocumentTask > 0)
                {
                    taskConstants.fetchDocumentTask--;
                }
                break;
            case InteractableType.ToDeliverDoc:
                if (taskConstants.deliverDocumentTask > 0)
                {
                    taskConstants.deliverDocumentTask--;
                }
                break;
            case InteractableType.ToChargeMic:
                if (taskConstants.chargeMicrophoneTask > 0)
                {
                    taskConstants.chargeMicrophoneTask--;
                }
                break;
            case InteractableType.ToPrepMeeting:
                if (taskConstants.prepMeetingMaterialsTask > 0)
                {
                    taskConstants.prepMeetingMaterialsTask--;
                }
                break;
            case InteractableType.ToPrepRefreshment:
                if (taskConstants.prepMeetingMaterialsTask > 0)
                {
                    taskConstants.prepMeetingMaterialsTask--;
                }
                break;
            case InteractableType.ToReturnDoc:
                if (taskConstants.returnDocumentTask > 0)
                {
                    taskConstants.returnDocumentTask--;
                }
                break;
        }
    }
}
