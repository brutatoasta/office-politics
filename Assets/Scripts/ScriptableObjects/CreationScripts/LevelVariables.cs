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
        Debug.Log(name);
        if (todo[(int)name].current > 0)
        {
            todo[(int)name].current--;

            // add performance points for that task
            levelPP += todo[(int)name].performancePoints;

            // no change to stress

            //if (todo[(int)name].quota == 0)
            //{
            //    todo[(int)name].StrikeOut();
            //}
        }
    }
    public void Fail(TaskName name)
    {
        // perhaps subtract PP or reset streak
        // levelPP -= todo[(int)name].performancePoints;
        // increase stress
        stressPoints += todo[(int)name].stressDamage;
        GameManager.instance.IncreaseStress();
    }

    public void Init(int level)
    {
        // let evadeType be determined by player
        stressPoints = 0;
        maxStressPoints = 50;
        levelPP = 0;
        todo = TaskConstants.todos[level];
        for (int i = 0; i < todo.Length; i++) 
        {
            todo[i].current = todo[i].quota;
        }
        Debug.Log(todo);
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

