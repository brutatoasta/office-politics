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
                new(1, 100, 5, TaskName.FetchTea, "Fetch tea for Siyang", "TaskIcons/FetchTea"),
                new(1, 100, 5, TaskName.FetchCoffee, "Fetch coffee for Joshua", "TaskIcons/FetchCoffee"),
            },
            // level 2
            new TaskItem[]
            {
                new(1, 100, 5, TaskName.FetchTea, "Fetch tea for Natthan", "TaskIcons/FetchTea"),
                new(1, 100, 5, TaskName.FetchCoffee, "Fetch coffee for Emmanuel", "TaskIcons/FetchCoffee"),
                new(2, 100, 5, TaskName.PrepRefreshment, "Prepare refreshment in the meeting room", "TaskIcons/PrepRefreshment"),
                new(3, 100, 5, TaskName.PrepMeeting, "Prepare Meeting Materials in the meeting room", "TaskIcons/PrepMeeting"),

            },
            // level 3
            new TaskItem[]
            {
                new(1, 100, 5, TaskName.Shred, "Shred documents in the BOSS room", "TaskIcons/Shred"),
                new(1, 100, 5, TaskName.Laminate, "Laminate documents in the BOSS room", "TaskIcons/Laminate"),
                new(1, 100, 5, TaskName.FetchDoc, "Fetch documents from Jolin for photocopy", "TaskIcons/DeliverDoc"),
                new(1, 100, 5, TaskName.DeliverDoc, "Deliver photocopied documents back to Jolin", "TaskIcons/DeliverDoc"),
            },
            // level 4
            new TaskItem[]
            {
                new(1, 100,5, TaskName.Shred, "Shred documents in the BOSS room", "TaskIcons/Shred"),
                new(1, 100,5, TaskName.Laminate, "Laminate documents in the BOSS room", "TaskIcons/Laminate"),
                new(2, 100, 5, TaskName.PrepRefreshment, "Prepare refreshment in the meeting room", "TaskIcons/PrepRefreshment"),
                new(3, 100, 5, TaskName.PrepMeeting, "Prepare Meeting Materials in the meeting room", "TaskIcons/PrepMeeting"),
                new(1, 100,5, TaskName.FetchDoc, "Fetch documents from Sihan for photocopy", "TaskIcons/DeliverDoc"),
                new(1, 100,5, TaskName.DeliverDoc, "Deliver photocopied documents back to Sihan", "TaskIcons/DeliverDoc"),
                new(1, 100,5, TaskName.RefillCoffee, "Refill the empty coffee pot", "TaskIcons/RefillCoffee"),
            }

       };

    public Material[] highlightMaterial;
    public Material defaultMaterial;

}
[Serializable]
public struct TaskItem
{
    public int quota;
    public int performancePoints;
    public int current;
    public int stressDamage;
    public TaskName taskName;
    public string taskString;
    public string taskIconPath;
    public TaskItem(int quota, int performancePoints, int stressDamage, TaskName taskName, string taskString, string taskIconPath)
    {
        this.quota = quota;
        this.current = quota;
        this.performancePoints = performancePoints;
        this.stressDamage = stressDamage;
        this.taskName = taskName;
        this.taskString = taskString;
        this.taskIconPath = taskIconPath;
        // this = new(); // mystery
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
    FetchDoc,
    DeliverDoc,
    PrepMeeting,
    PrepRefreshment,
    emptyCoffeePot,
    Default,
}