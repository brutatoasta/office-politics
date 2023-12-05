using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskGenerator : MonoBehaviour
{
    // attached to tmp gameobject for task list UI
    TextMeshProUGUI taskList;
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
        int yPos = 0;
        foreach (TaskItem taskItem in GameManager.instance.levelVariables.todo)
        {
            GameObject iconObj = Instantiate(iconsPrefab, new Vector3(0, -80 * yPos,0) + iconsParent.transform.position, Quaternion.identity);
            iconObj.GetComponent<Image>().sprite = Resources.Load<Sprite>(taskItem.taskIconPath);
            iconObj.transform.SetParent(iconsParent.transform);

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
            yPos++;
        }
        taskList.text = description;
    }



}
