using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "EndingVariables", menuName = "ScriptableObjects/EndingVariables")]
public class EndingVariables : ScriptableObject
{
    // Tracks dynamic values for each Level
    public bool Slacker;
    public bool Ninja;
    public bool SustainableWarrior;
    public bool BigEarner;
    public bool Workaholic;
    public int OTCount;
    public bool TaichiMaster;
    public int stunCount;
    public bool Perfectionist;
    public int successCount;
    public bool Reincarnation;
    public int finalScore;

    public void Init()
    {
        Slacker = false;
        Ninja = true;
        SustainableWarrior = true;
        BigEarner = false;
        Workaholic = false;
        OTCount = 0;
        TaichiMaster = false;
        stunCount = 0;
        Perfectionist = false;
        successCount = 0;
        Reincarnation = false;
    }

    public int GetAchivementCount()
    {
        int achievementCount = 0;

        if (Slacker) achievementCount++;
        if (Ninja) achievementCount++;
        if (SustainableWarrior) achievementCount++;
        if (BigEarner) achievementCount++;
        if (Workaholic) achievementCount++;
        if (TaichiMaster) achievementCount++;
        if (Perfectionist) achievementCount++;
        if (Reincarnation) achievementCount++;

        return achievementCount;
    }

    public string generateDescription()
    {
        if (successCount >= 10)
        {
            Perfectionist = true;
        }
        if (stunCount >= 5)
        {
            TaichiMaster = true;
        }
        if (OTCount >= 3)
        {
            Workaholic = true;
        }
        if (Slacker && Ninja && SustainableWarrior && BigEarner && Workaholic && TaichiMaster && Perfectionist)
        {
            Reincarnation = true;
        }

        string description = "";
        if (Slacker)
        {
            description += "\nSlacker    -------------------------------------------------------------------    $500\n";
            description += "(Finish while the clock is still green)\n";
        }
        if (Ninja)
        {
            description += "\nNinja    ----------------------------------------------------------------------    $500\n";
            description += "(Get hit by zero arrow)\n";
        }
        if (SustainableWarrior)
        {
            description += "\nSustainable Warrior    ---------------------------------------------------------    $500\n";
            description += "(Throw nothing into the trash)\n";
        }
        if (BigEarner)
        {
            description += "\nBig Earner    ----------------------------------------------------------------    $500\n";
            description += "(Earn 1000PP in one day)\n";
        }
        if (Workaholic)
        {
            description += "\nWorkaholic    ---------------------------------------------------------------     $500\n";
            description += "(OT 3 or more times)\n";
        }
        if (TaichiMaster)
        {
            description += "\nTaichi Master    -------------------------------------------------------------    $500\n";
            description += "(Stun the boss more than 5 times)\n";
        }
        if (Perfectionist)
        {
            description += "\nPerfectionist    --------------------------------------------------------------    $500\n";
            description += "(Complete tasks without failure 10 times in a row)\n";
        }
        if (Reincarnation)
        {
            description += "\nThat Time I got Reincarnated as an Intern    ------------------------------------    $500\n";
            description += "(Get all the achievements in a single playthrough)\n";
        }
        return description;

    }
    public string GetPosition()
    {

        int achievementCount = GetAchivementCount();

        if (achievementCount >= 0 && achievementCount <= 2)
        {
            return "Executive Intern";
        }
        else if (achievementCount >= 3 && achievementCount <= 5)
        {
            return "Associate";
        }
        else if (achievementCount >= 6 && achievementCount <= 7)
        {
            return "Specialist";
        }
        else
        {
            return "Vice President";
        }
    }
    public int ScoreCount()
    {
        int achievementCount = GetAchivementCount();
        finalScore = achievementCount * 500 + 5000;
        return finalScore;
    }

}
