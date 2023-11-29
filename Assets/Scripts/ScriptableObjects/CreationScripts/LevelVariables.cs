using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "LevelVariables", menuName = "ScriptableObjects/LevelVariables")]
public class LevelVariables : ScriptableObject
{
    // Tracks dynamic values for each Level
    public EvadeType evadeType;
    [SerializeField]
    public TaskItem[] todo;

    public int levelPP;
    public int stressPoints;
    public int maxStressPoints;
    public int currentLevelIndex;
    public int currentSceneIndex;
    public bool isQuotaComplete()
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
                }
                break;
            }
        }
    }
    public void Fail(TaskName name)
    {
        // perhaps subtract PP or reset streak
        // levelPP -= todo[(int)name].performancePoints;
        // increase stress
        //stressPoints += todo[(int)name].stressDamage;
        //GameManager.instance.IncreaseStress();
        for (int i = 0; i < todo.Length; i++)
        {
            if (todo[i].taskName.Equals(name))
            {
                stressPoints += todo[i].stressDamage;
                GameManager.instance.IncreaseStress();
                break;
            }
        }
    }

    public void Init()
    {
        // let evadeType be determined by player
        stressPoints = 0;
        maxStressPoints = 50;
        todo = TaskConstants.todos[currentLevelIndex];
        for (int i = 0; i < todo.Length; i++)
        {
            todo[i].current = todo[i].quota;
        }
        // Debug.Log(todo);
    }

    public void addRandomJob(int level)
    {
        todo = TaskConstants.todos[level];

        // randomly select an index
        int i = UnityEngine.Random.Range(0, todo.Length);
        todo[i].current++;
    }

    public void ExitLevel()
    {
        // transfer to RunVariables
        GameManager.instance.runVariables.performancePoints += levelPP;
    }

}

