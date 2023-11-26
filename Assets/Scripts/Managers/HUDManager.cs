using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{


    public Image current;
    public Image next;
    public CanvasGroup currentGroup;
    public CanvasGroup nextGroup;

    private int currSlot;
    private int nextSlot;
    public CanvasGroup fade;
    public GameObject countText;
    public Slider slider;
    public PlayerConstants playerConstants;
    public GameObject performancePointText;
    public GameObject taskList;
    public GameObject gameOverScreen;
    public bool isShown = false;
    public Animator taskAnimator;
    // public bool ExitState = false;
    // public Animator taskAnimator;

    void Start()
    {
        GameManager.instance.cycleInventory.AddListener(CycleInventory);
        GameManager.instance.useConsumable.AddListener(UseConsumable);
        GameManager.instance.increaseStress.AddListener(StressBarSlider);
        GameManager.instance.showPerformancePoint.AddListener(PerformancePoint);
        GameManager.instance.gameOver.AddListener(OnGameOver);


        current.sprite = GameManager.instance.runVariables.consumableObjects[0].sprite;
        next.sprite = GameManager.instance.runVariables.consumableObjects[1].sprite;

        currSlot = 0;
        nextSlot = 1;

        countText.GetComponent<TextMeshProUGUI>().text = "" + GameManager.instance.runVariables.consumableObjects[0].count;
    }

    public void CycleInventory(int currentInventorySlot)
    {
        current.sprite = (GameManager.instance.runVariables.consumableObjects[currentInventorySlot].count > 0) ?
                            GameManager.instance.runVariables.consumableObjects[currentInventorySlot].sprite :
                            null;

        int nextInventorySlot = (currentInventorySlot + 1) % GameManager.instance.runVariables.consumableObjects.Length;
        for (int i = 0; i < GameManager.instance.runVariables.consumableObjects.Length; i++)
        {
            if (GameManager.instance.runVariables.consumableObjects[nextInventorySlot].count != 0) break;
            nextInventorySlot = (nextInventorySlot + 1) % GameManager.instance.runVariables.consumableObjects.Length;
        }
        next.sprite = (GameManager.instance.runVariables.consumableObjects[nextInventorySlot].count > 0 && nextInventorySlot != currentInventorySlot) ?
                            GameManager.instance.runVariables.consumableObjects[nextInventorySlot].sprite :
                            null;

        currSlot = currentInventorySlot;
        nextSlot = nextInventorySlot;

        currentGroup.alpha = (current.sprite == null) ? 0 : 1;
        nextGroup.alpha = (next.sprite == null) ? 0 : 1;

        countText.GetComponent<TextMeshProUGUI>().text = "" + GameManager.instance.runVariables.consumableObjects[currentInventorySlot].count;
    }

    public void UseConsumable()
    {
        current.sprite = (GameManager.instance.runVariables.consumableObjects[currSlot].count > 0) ?
                            GameManager.instance.runVariables.consumableObjects[currSlot].sprite :
                            null;
        next.sprite = (GameManager.instance.runVariables.consumableObjects[nextSlot].count > 0 && nextSlot != currSlot) ?
                            GameManager.instance.runVariables.consumableObjects[nextSlot].sprite :
                            null;

        currentGroup.alpha = (current.sprite == null) ? 0 : 1;
        nextGroup.alpha = (next.sprite == null) ? 0 : 1;
        StartCoroutine(Fade());

        countText.GetComponent<TextMeshProUGUI>().text = "" + GameManager.instance.runVariables.consumableObjects[currSlot].count;
    }

    IEnumerator Fade()
    {
        for (float alpha = 0.5f; alpha >= 0f; alpha -= 0.05f)
        {
            fade.alpha = alpha;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
    public void StressBarSlider()
    {
        slider.value = GameManager.instance.levelVariables.stressPoints;
    }
    public void PerformancePoint()
    {
        performancePointText.GetComponent<TextMeshProUGUI>().text = "Performance Point: " + GameManager.instance.levelVariables.levelPP;
    }

    public void TaskList()
    {
        // taskList.SetActive(true);
        if (isShown)
        {
            Time.timeScale = 1;
            isShown = false;
            taskAnimator.Play("TaskExit");


        }
        else
        {
            GameManager.instance.onTaskSuccess.Invoke();
            isShown = true;
            taskAnimator.Play("TasksPage");

        }
    }
    public void FreezeTime()
    {
        Time.timeScale = 0;
    }
    public void DisableTask()
    {
        taskList.SetActive(false);

    }
    public void EnableTask()
    {
        taskList.SetActive(true);

    }


    public void OnGameOver()
    {
        gameOverScreen.SetActive(true);
    }

}
