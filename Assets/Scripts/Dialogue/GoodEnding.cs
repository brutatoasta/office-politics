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
        trigger.StartDialogue();
    }
    public void GenerateLetter()

    {
        string position = endingVariables.GetPosition();
        string achievementsDescription = endingVariables.generateDescription();
        int compensation = endingVariables.ScoreCount();
        string description = "Dear Intern #24601, \nWe are delighted to offer you the position of " + position +
         " at TaiChill Games. We have been thoroughly impressed with your performance during your internship, and we believe that your skills and talents will contribute significantly to our team's success.\n";
        description += "Base Salary                             $5000 \n";
        description += achievementsDescription;
        description += "Final Compensation                      $";
        description += compensation;
        achievementList.text = description;
    }
    public void ShowLetterIcon()
    {
        letterIcon.SetActive(true);
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

