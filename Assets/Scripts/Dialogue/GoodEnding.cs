using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GoodEnding : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueTrigger trigger;
    public GameObject gridScene;
    public GameObject phoneIcon;
    public Animator phoneAnimation;
    public Animator sceneAnimation;
    public TextMeshProUGUI achievementList;
    public EndingVariables endingVariables;
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
        string achievementsDescription = endingVariables.generateDescription();
        int compensation = endingVariables.ScoreCount();
        string description = "Dear Intern #24601, \nWe are delighted to offer you the position of " + position +
         " at TaiChill Games. We have been thoroughly impressed with your performance during your internship, and we believe that your skills and talents will contribute significantly to our team's success.\n\n";
        description += "Monthly Salary\n";
        description += "--------------------------------------------------------------------------------------\n";
        description += "Base Salary    ---------------------------------------------------------------    $5000\n";
        description += achievementsDescription;
        description += "\nTotal Compensation    --------------------------------------------------------    $";
        description += compensation;
        description += "\n\n Please sign and return the duplicate copy of this letter in confirmation of your understanding and acceptance.\n";
        description += "\n Yours Sincerely";
        description += "\n TaiChill Games";
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
        SceneManager.LoadSceneAsync("MainMenu");
    }

}

