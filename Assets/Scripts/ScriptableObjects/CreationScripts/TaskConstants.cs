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
                new(1, 1, 5, TaskName.Laminate, "Laminate documents"),
                new(1, 1, 5, TaskName.PrepMeeting, "Laminate documents"),
            },
            // level 2
            new TaskItem[]
            {
                new(1, 1, 5, TaskName.Shred, "Shred documents"),
                new(1, 1, 5, TaskName.Laminate, "Laminate documents"),

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