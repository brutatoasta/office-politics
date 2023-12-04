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
                new(1, 100, 5, TaskName.FetchTea, "Fetch tea for Siyang"),
                new(1, 100, 5, TaskName.FetchCoffee, "Fetch coffee for Joshua"),
            },
            // level 2
            new TaskItem[]
            {
                new(1, 100, 5, TaskName.FetchTea, "Fetch tea for Natthan"),
                new(1, 100, 5, TaskName.FetchCoffee, "Fetch coffee for Emmanuel"),
                new(2, 100, 5, TaskName.PrepRefreshment, "Prepare refreshment in the meeting room"),
                new(3, 100, 5, TaskName.PrepMeeting, "Prepare Meeting Materials in the meeting room"),

            },
            // level 3
            new TaskItem[]
            {
                new(1, 100, 5, TaskName.Shred, "Shred documents in the BOSS room"),
                new(1, 100, 5, TaskName.Laminate, "Laminate documents in the BOSS room"),
                new(1, 100, 5, TaskName.FetchDoc, "Fetch documents from Jolin for photocopy"),
                new(1, 100, 5, TaskName.DeliverDoc, "Deliver photocopied documents back to Jolin"),
            },
            // level 4
            new TaskItem[]
            {
                new(1, 100,5, TaskName.Shred, "Shred documents in the BOSS room"),
                new(1, 100,5, TaskName.Laminate, "Laminate documents in the BOSS room"),
                new(2, 100, 5, TaskName.PrepRefreshment, "Prepare refreshment in the meeting room"),
                new(3, 100, 5, TaskName.PrepMeeting, "Prepare Meeting Materials in the meeting room"),
                new(1, 100,5, TaskName.FetchDoc, "Fetch documents from Sihan for photocopy"),
                new(1, 100,5, TaskName.DeliverDoc, "Deliver photocopied documents back to Sihan"),
                new(1, 100,5, TaskName.RefillCoffee, "Refill the empty coffee pot"),
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
    public TaskItem(int quota, int performancePoints, int stressDamage, TaskName taskName, string taskString)
    {
        this.quota = quota;
        this.current = quota;
        this.performancePoints = performancePoints;
        this.stressDamage = stressDamage;
        this.taskName = taskName;
        this.taskString = taskString;
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
    Default,
}