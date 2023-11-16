using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    // tracks tasks and score
    public TaskConstants taskConstants;
    private int score = 0; // TODO: read from/ integrate scoremanager
    private TaskItem[] todo;
    public void Succeed(TaskName name)
    {
        // reduce quota
        if (todo[(int)name].quota > 0)
        {
            todo[(int)name].quota--;

            // add performance points for that task
            score += todo[(int)name].performancePoint;
        }

    }
    public void Fail(TaskName name)
    {
        // subtract performance points
        score -= todo[(int)name].performancePoint;
    }

    void Start()
    {
        GameManager.instance.switchTasks.AddListener(Succeed); // TODO: 2 different unity events?
        GameManager.instance.switchTasks.AddListener(Fail);
        // select some 
        GameStart();

    }
    public void GameStart()
    {
        // TODO: get level
        todo = taskConstants.todos[0];
    }
}
