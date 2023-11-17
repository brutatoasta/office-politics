using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

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
    void generateDescription()
    {
        string description = "";
        foreach (TaskItem taskItem in GameManager.instance.levelVariables.todo)
        {
            description += taskItem.taskString;
        }
        taskList.text = description;
    }



}
