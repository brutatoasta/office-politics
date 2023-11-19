using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskConstants", menuName = "ScriptableObjects/TaskConstants")]
public class TaskConstants : ScriptableObject
{
    // this is the readonly stuff
    // each TaskItem array must have TaskItems in order and at the index of enum as they are accessed via enum, and 
    // TODO: read from CSV
    // unity does not serialize multidimensional arrays/matrices
    public static TaskItem[][] todos = new TaskItem[][]
       {
            // level 1
            
            new TaskItem[]
            {
                new(1, 1, 5, TaskName.Shred, "Shred documents"),
                // new(1, 1, 5, TaskName.Laminate, "Laminate documents"),

                // new(1, 1, 5, TaskName.FetchCoffee, "Fetch coffee for Mary"),
                // new(1, 1, 5, TaskName.RefillCoffee, "Refill the empty coffee pot"),
                // new(1, 1, 5, TaskName.FetchTea, "Fetch tea for Ann"),
                // new(1, 1, 5, TaskName.RefillTea, "Refill the empty tea pot"),

                // new(1, 1, 5, TaskName.FetchDoc, "Fetch documents from Jack for photocopy"),
                // new(1, 1, 5, TaskName.DeliverDoc, "Deliver photocopied documents to Tom"),

                // new(1, 1, 5, TaskName.ChargeMic, "Charge mic in the meeting room"),
                // new(1, 1, 5, TaskName.PrepRefreshment, "Prepare refreshment in the meeting room"),
                // new(1, 1, 5, TaskName.PrepMeeting, "Prepare Meeting Materials in the meeting room"),


            },
            // level 2
            new TaskItem[]
            {
                new(1, 1, 5, TaskName.Shred, "Shred documents"),
                new(1, 1, 5, TaskName.Laminate, "Laminate documents"),
                new(1, 1, 5, TaskName.PrepMeeting, "Prepare Meeting Materials"),


            }
       };

    public Material highlightMaterial;
    public Material defaultMaterial;

}
[Serializable]
public struct TaskItem
{
    public int quota;
    public int performancePoints;

    public int stressDamage;
    public TaskName taskName;
    public string taskString;
    public TaskItem(int quota, int performancePoints, int stressDamage, TaskName taskName, string taskString)
    {
        this.quota = quota;
        this.performancePoints = performancePoints;
        this.stressDamage = stressDamage;
        this.taskName = taskName;
        this.taskString = taskString;
        // this = new(); // mystery
    }
    public void StrikeOut()
    {
        const string STRIKE_START = "<s>";
        const string STRIKE_END = "</s>";
        taskString = STRIKE_START + taskString + STRIKE_END;
    }

}

// Name of task. We have separate enum for the interactable type and taskname because one task can have multiple interactions required.
// 
public enum TaskName
{
    Shred,
    Laminate,

    FetchCoffee,
    RefillCoffee,
    FetchTea,
    RefillTea,

    FetchDoc,
    DeliverDoc,

    ChargeMic,
    PrepMeeting,
    PrepRefreshment,

    Default,
}