using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskGenerator : MonoBehaviour
{
    // attached to tmp gameobject for task list UI
    TextMeshProUGUI taskList;
    bool iconIsGenerated;
    public GameObject iconsParent;
    public GameObject iconsPrefab;
    void Start()
    {
        taskList = GetComponent<TextMeshProUGUI>();
        GenerateDescription();
        GameManager.instance.onTaskSuccess.AddListener(GenerateDescription);
    }
    private string StrikeOut(string text)
    {
        const string STRIKE_START = "<s>";
        const string STRIKE_END = "</s>";
        return STRIKE_START + text + STRIKE_END;
    }
    void GenerateDescription()
    {
        string description = "To Do: \n";
        int yPos = -5;
        foreach (TaskItem taskItem in GameManager.instance.levelVariables.todo)
        {
            if (!iconIsGenerated)
            {
                GameObject iconObj = Instantiate(iconsPrefab, new Vector3(0, yPos,0) + iconsParent.transform.position, Quaternion.identity);
                iconObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(taskItem.taskIconPath);
                iconObj.transform.SetParent(iconsParent.transform);
            }

            string description_to_add = "";
            if (taskItem.current == 0)
            {
                description_to_add += StrikeOut(taskItem.taskString);
            }
            else if (taskItem.current == 1) 
            {
                description_to_add += taskItem.taskString;
            }
            else
            {
                description_to_add += taskItem.taskString + $" x{taskItem.current}";
            }
            if (description_to_add.Length <= 24)
            {
                yPos -= 70;
            }
            else
            {
                yPos -= 115;
            }
            description += description_to_add + "\n";
            yPos++;
        }
        taskList.text = description;
        iconIsGenerated = true;
    }



}
