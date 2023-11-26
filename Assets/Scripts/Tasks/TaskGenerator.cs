using System;
using UnityEngine;
using TMPro;

public class TaskGenerator : MonoBehaviour
{
    // attached to tmp gameobject for task list UI
    TextMeshProUGUI taskList;
    void Start()
    {
        taskList = GetComponent<TextMeshProUGUI>();
        generateDescription();
        GameManager.instance.onTaskSuccess.AddListener(generateDescription);
    }
    private string StrikeOut(string text)
    {
        const string STRIKE_START = "<s>";
        const string STRIKE_END = "</s>";
        return STRIKE_START + text + STRIKE_END;
    }
    void generateDescription()
    {
        string description = "To Do: \n";
        foreach (TaskItem taskItem in GameManager.instance.levelVariables.todo)
        {
            if (taskItem.current == 0)
            {
                description += StrikeOut(taskItem.taskString);
            }
            else if (taskItem.current == 1) 
            {
                description += taskItem.taskString;
            }
            else
            {
                description += taskItem.taskString + $" x{taskItem.current}";
            }
            description += "\n";
        }
        taskList.text = description;
    }



}
