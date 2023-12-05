using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GoodEnding : MonoBehaviour
{

    public DialogueTrigger trigger;
    public GameObject gridScene;
    public GameObject phoneIcon;
    public Animator phoneAnimation;
    public Animator sceneAnimation;
    public TextMeshProUGUI achievementList;
    public EndingVariables endingVariables;
    public RunVariables runVariables;
    public GameObject letter;
    public GameObject letterIcon;
    public GameObject dialogueBox;
    public void PickupCall()
    {
        phoneIcon.SetActive(false);
        gridScene.SetActive(true);
        sceneAnimation.SetTrigger("PickupCall");

    }
    public void Dialogue()
    {
        trigger.StartDialogue();
    }
    public void GenerateLetter()

    {
        string position = endingVariables.GetPosition();
        string achievementsDescription = endingVariables.GenerateDescription();
        float ppcompensation = runVariables.performancePoints * endingVariables.performancePointsCompensation;
        float compensation = endingVariables.ScoreCount() + ppcompensation;
        string description = "Dear Intern #24601, \nWe are delighted to offer you the position of " + position +
         " at TaiChill Games. We have been thoroughly impressed with your performance during your internship, and we believe that your skills and talents will contribute significantly to our team's success.\n\n";
        description += "Monthly Salary\n";
        description += "--------------------------------------------------------------------------------------\n";
        description += "Base Salary    ----------------------------------------------------------------    $5000\n";
        description += achievementsDescription;
        description += "\nPerformance Point Compensation    ----------------------------------------------    $";
        description += ppcompensation;
        description += "\n\nTotal Compensation    ----------------------------------------------------------    $";
        description += compensation;
        description += "\n\nPlease sign and return the duplicate copy of this letter in confirmation of your understanding and acceptance.\n";
        description += "\n\nYours Sincerely";
        description += "\nTaiChill Games";
        achievementList.text = description;
    }
    public void ShowLetterIcon()
    {
        letterIcon.SetActive(true);
        phoneAnimation.Play("letter");
        dialogueBox.SetActive(false);
    }
    public void ShowLetter()
    {
        letterIcon.SetActive(false);
        letter.SetActive(true);
        GenerateLetter();
    }
    public void AcceptOffer()
    {
        endingVariables.firstTimeGame = false;
        SceneManager.LoadSceneAsync("MainMenu");
    }


}

