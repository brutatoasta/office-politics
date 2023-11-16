using System;
using UnityEngine;

[Serializable]
public struct TaskItem
{
    public int quota;
    public TaskName taskName;
}

public enum TaskName
{
    Shred,
    Laminate,
}