using System;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(fileName = "TaskConstants", menuName = "ScriptableObjects/TaskConstants", order = 6)]
public class TaskConstants : ScriptableObject
{
    // this is the readonly stuff
    public readonly TaskItem[][] todos = new TaskItem[][]
   {
        // level 1
        new TaskItem[]
        {
            new TaskItem(1, 1, TaskName.Shred),
            new TaskItem(1, 1, TaskName.Laminate),
            new TaskItem(1, 1, TaskName.Shred),
            new TaskItem(1, 1, TaskName.Shred),
        },
        // level 2
        new TaskItem[]
        {
            new TaskItem(1, 1, TaskName.Shred),
            new TaskItem(1, 1, TaskName.Laminate),
            new TaskItem(1, 1, TaskName.Shred),
            new TaskItem(1, 1, TaskName.Shred),
        }
   };

    public static void readFrom()
    {
        foreach (var name in Enum.GetNames(typeof(TaskName)))
        {
            Console.WriteLine(name);
        }
    }

}
public struct TaskItem
{
    public int quota;
    public readonly int performancePoint;
    public TaskName taskName;
    public TaskItem(int quota, int performancePoint, TaskName taskName)
    {
        this.quota = quota;
        this.performancePoint = performancePoint;
        this.taskName = taskName;
    }

}

// Name of task. We have separate enum for the interactable type and taskname because one task can have multiple interactions required.
// 
public enum TaskName
{
    Shred,
    Laminate,
}
