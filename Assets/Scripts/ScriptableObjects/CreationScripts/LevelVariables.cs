using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "LevelVariables", menuName = "ScriptableObjects/LevelVariables")]
public class LevelVariables : ScriptableObject
{
    // Tracks dynamic values for each Level
    public EvadeType evadeType;

    public TaskItem[] todo;

    public int levelPP;
    public int stressPoints;
    public int maxStressPoints;

    public bool isQuotaComplete()
    {
        foreach (TaskItem taskItem in todo)
        {
            if (taskItem.quota > 0)
            {
                return false;
            }
        }
        return true;
    }
    public void Succeed(TaskName name)
    {
        // reduce quota
        if (todo[(int)name].quota > 0)
        {
            todo[(int)name].quota--;

            // add performance points for that task
            levelPP += todo[(int)name].performancePoints;

            // no change to stress

            if (todo[(int)name].quota == 0)
            {
                todo[(int)name].StrikeOut();
            }
        }
    }
    public void Fail(TaskName name)
    {
        // perhaps subtract PP or reset streak
        // levelPP -= todo[(int)name].performancePoints;
        // increase stress
        stressPoints += todo[(int)name].stressDamage;
    }

    public void Init(int level)
    {
        // let evadeType be determined by player
        stressPoints = 0;
        maxStressPoints = 199;
        levelPP = 0;
        todo = TaskConstants.todos[level];
    }

    public void ExitLevel()
    {
        // transfer to RunVariables
        GameManager.instance.runVariables.performancePoints += levelPP;
    }

}

