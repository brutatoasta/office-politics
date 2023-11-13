using UnityEngine;

[CreateAssetMenu(fileName = "TaskConstants", menuName = "ScriptableObjects/TaskConstants", order = 6)]
public class TaskConstants : ScriptableObject
{
    public int fetchCoffeeTask;
    public int refillCoffeeTask;
    public int fetchTeaTask;
    public int fetchDocumentTask;
    public int deliverDocumentTask;
    public int chargeMicrophoneTask;

    public int prepMeetingMaterialsTask;
    public int refereshmentsTask;
    public int collectDocumentsTask;

    public int shredDocumentsTask;
    public int laminateDocumentTask;
    public int returnDocumentTask;
    public InteractableType currentInput;



}
