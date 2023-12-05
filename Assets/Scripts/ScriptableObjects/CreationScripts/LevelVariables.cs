using UnityEngine;

[CreateAssetMenu(fileName = "LevelVariables", menuName = "ScriptableObjects/LevelVariables")]
public class LevelVariables : ScriptableObject
{
    // Tracks dynamic values for each Level
    public EvadeType evadeType;
    public TaskItem[] todo;

    public int levelPP;
    public int stressPoints;
    public int maxStressPoints;
    public int currentLevelIndex;
    public bool IsQuotaComplete()
    {
        foreach (TaskItem taskItem in todo)
        {
            if (taskItem.current > 0)
            {
                return false;
            }
        }
        return true;
    }
    public void Succeed(TaskName name)
    {
        // reduce quota
        Debug.Log((int)name);
        Debug.Log(todo);
        Debug.Log(todo[0]);
        for (int i = 0; i < todo.Length; i++)
        {
            if (todo[i].taskName.Equals(name))
            {
                if (todo[i].current > 0)
                {
                    todo[i].current--;
                    // add performance points for that task
                    levelPP += todo[i].performancePoints;
                    GameManager.instance.animatePerformancePoint.Invoke(todo[i].performancePoints);

                    if (todo[i].current > 0)
                    {
                        // sfx for reduce task quota
                        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.reduceTaskQuota);
                    }
                    else if (todo[i].current == 0)
                    {
                        // sfx for task complete
                        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.taskComplete);
                    }
                }
                break;
            }
        }
        if (levelPP >= 1000)
        {
            GameManager.instance.endingVariables.BigEarner = true;
        }
        GameManager.instance.endingVariables.successCount++;
    }
    public void Fail(TaskName name)
    {
        for (int i = 0; i < todo.Length; i++)
        {
            if (todo[i].taskName.Equals(name))
            {
                stressPoints += todo[i].stressDamage;
                levelPP -= 50;
                GameManager.instance.animatePerformancePoint.Invoke(-50);
                GameManager.instance.IncreaseStress();
                break;
            }
        }
        if (GameManager.instance.endingVariables.successCount <= 10)
        {
            GameManager.instance.endingVariables.successCount = 0;
        }
    }

    public void Init()
    {
        // let evadeType be determined by player
        levelPP = 0;
        stressPoints = 0;
        todo = TaskConstants.todos[currentLevelIndex];
        for (int i = 0; i < todo.Length; i++)
        {
            todo[i].current = todo[i].quota;
        }
        Debug.Log(todo);
    }

    public void AddRandomJob(int level)
    {
        todo = TaskConstants.todos[level];

        // randomly select an index
        int i = Random.Range(0, todo.Length);
        todo[i].current++;
        GameManager.instance.animateTaskAdd.Invoke(todo[i]);
    }

    public void AddCoffeeJob()
    {
        for (int i = 0; i < todo.Length; i++)
        {
            if (todo[i].taskName.Equals(TaskName.FetchCoffee))
            {
                todo[i].current++;
                GameManager.instance.animateTaskAdd.Invoke(todo[i]);
                break;
            }
        }
    }

    public void ExitLevel()
    {
        // transfer to RunVariables
        GameManager.instance.runVariables.performancePoints += levelPP;
    }

}

