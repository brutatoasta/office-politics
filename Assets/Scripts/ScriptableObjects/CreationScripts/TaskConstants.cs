using System;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskConstants", menuName = "ScriptableObjects/TaskConstants")]
public class TaskConstants : ScriptableObject
{
    // this is the readonly stuff
    public static readonly TaskItem[][] todos = new TaskItem[][]
   {
        // level 1
        new TaskItem[]
        {
            new TaskItem(1, 1, 5, TaskName.Shred, "Shred documents"),
            new TaskItem(1, 1, 5, TaskName.Laminate, "Laminate documents"),
        },
        // level 2
        new TaskItem[]
        {
            new TaskItem(1, 1, 5, TaskName.Shred, "Shred documents"),
            new TaskItem(1, 1, 5, TaskName.Laminate, "Laminate documents"),

        }
   };

}
public struct TaskItem
{
    public int quota;
    public readonly int performancePoints;

    public readonly int stressDamage;
    public TaskName taskName;
    public string taskString;
    public TaskItem(int quota, int performancePoints, int stressDamage, TaskName taskName, string taskString)
    {
        this.quota = quota;
        this.performancePoints = performancePoints;
        this.stressDamage = stressDamage;
        this.taskName = taskName;
        this.taskString = taskString;
        this = new(); // mystery
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
}
