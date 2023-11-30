using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{



    public CanvasGroup[] fades;
    public GameObject countText;
    public Slider slider;
    public PlayerConstants playerConstants;
    public GameObject performancePointText;
    public GameObject taskList;
    public GameObject gameOverScreen;
    public bool isShown = false;
    public Animator taskAnimator;

    public GameObject[] shopCounts;
    public GameObject performancePointsShop;
    public GameObject shopUI;
    // public bool ExitState = false;
    // public Animator taskAnimator;

    public Slider cooldownSlider;
    public Slider cooldownBlockSlider;
    public Image evadeIcon;
    public GameObject upgradeOutline;
    public GameObject unUpgradedGroup;
    public GameObject upgradedGroup;

    void Start()
    {
        GameManager.instance.updateInventory.AddListener(UpdateInventory);
        GameManager.instance.useConsumable.AddListener(UseConsumable);
        GameManager.instance.consumableEfffect.AddListener(UpdateCooldownBlock);

        GameManager.instance.increaseStress.AddListener(StressBarSlider);
        GameManager.instance.showPerformancePoint.AddListener(PerformancePoint);
        GameManager.instance.gameOver.AddListener(OnGameOver);
        GameManager.instance.playerEvade.AddListener(OnPlayerEvade);
        GameManager.instance.updateEvade.AddListener(UpdateCooldownSprite);

        UpdateShop();
        UpdateInventory();
        UpdateCooldownSprite(GameManager.instance.levelVariables.evadeType);

        // current.sprite = GameManager.instance.runVariables.consumableObjects[0].sprite;
        // next.sprite = GameManager.instance.runVariables.consumableObjects[1].sprite;

        // currSlot = 0;
        // nextSlot = 1;

        // countText.GetComponent<TextMeshProUGUI>().text = "" + GameManager.instance.runVariables.consumableObjects[0].count;
    }

    public void UpdateInventory()
    {
        if (GameManager.instance.runVariables.upgradeBought)
        {
            for (int i = 0; i < 3; i++)
            {
                Image slotImg = upgradedGroup.transform.GetChild(i).GetChild(1).GetComponent<Image>();
                CanvasGroup slotGroup = upgradedGroup.transform.GetChild(i).GetChild(1).GetComponent<CanvasGroup>();
                if (GameManager.instance.activeSlots[i] != -1)
                {
                    slotImg.sprite = GameManager.instance.runVariables.consumableObjects[GameManager.instance.activeSlots[i]].sprite;
                    slotGroup.alpha = 1;
                }
                else
                {
                    slotImg.sprite = null;
                    slotGroup.alpha = 0;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                upgradedGroup.transform.GetChild(i).GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + (
                    GameManager.instance.activeSlots[i] == -1 ?
                        0 :
                        GameManager.instance.runVariables.consumableObjects[GameManager.instance.activeSlots[i]].count
                );
            }

        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                Image slotImg = unUpgradedGroup.transform.GetChild(i).GetChild(1).GetComponent<Image>();
                CanvasGroup slotGroup = unUpgradedGroup.transform.GetChild(i).GetChild(1).GetComponent<CanvasGroup>();
                if (GameManager.instance.activeSlots[i] != -1)
                {
                    slotImg.sprite = GameManager.instance.runVariables.consumableObjects[GameManager.instance.activeSlots[i]].sprite;
                    slotGroup.alpha = 1;
                }
                else
                {
                    slotImg.sprite = null;
                    slotGroup.alpha = 0;
                }
            }
            unUpgradedGroup.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + (
                GameManager.instance.activeSlots[0] == -1 ?
                    0 :
                    GameManager.instance.runVariables.consumableObjects[GameManager.instance.activeSlots[0]].count
            );
        }
    }

    public void UseConsumable(int slot)
    {

        StartCoroutine(Fade(GameManager.instance.runVariables.upgradeBought ? slot : 0));


        UpdateShop();
        UpdateInventory();
    }

    IEnumerator Fade(int slot)
    {
        for (float alpha = 0.5f; alpha >= -0.05f; alpha -= 0.05f)
        {
            fades[slot].alpha = alpha;
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

            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.hideTaskDetails);
        }
        else
        {
            GameManager.instance.onTaskSuccess.Invoke();
            isShown = true;
            taskAnimator.Play("TasksPage");

            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.showTaskDetails);
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


    public void UpdateEvade(bool isDash)
    {
        GameManager.instance.UpdateEvadeType(isDash ? EvadeType.Dash : EvadeType.Parry);
    }

    public void BuyUpgrade()
    {
        if (
            !GameManager.instance.runVariables.upgradeBought &&
            GameManager.instance.runVariables.performancePoints >= 50
        )
        {
            GameManager.instance.runVariables.upgradeBought = true;
            GameManager.instance.runVariables.performancePoints -= 50;
            GameManager.instance.activeSlots.Add(0);
            GameManager.instance.CycleInventory();
        }
        UpdateShop();
    }

    public void BuyConsumable(int consumableIndex)
    {
        if (GameManager.instance.runVariables.performancePoints >= GameManager.instance.runVariables.consumableObjects[consumableIndex].cost)
        {
            GameManager.instance.runVariables.consumableObjects[consumableIndex].count += 1;
            GameManager.instance.runVariables.performancePoints -= GameManager.instance.runVariables.consumableObjects[consumableIndex].cost;
        }

        GameManager.instance.updateInventory.Invoke();
        UpdateShop();

    }

    public void UpdateShop()
    {
        for (int i = 0; i < GameManager.instance.runVariables.consumableObjects.Length; i++)
        {
            shopCounts[i].GetComponent<TextMeshProUGUI>().text = "x" + GameManager.instance.runVariables.consumableObjects[i].count;
        }

        performancePointsShop.GetComponent<TextMeshProUGUI>().text = "Owned: " + GameManager.instance.runVariables.performancePoints + " PP";


        upgradeOutline.SetActive(GameManager.instance.runVariables.upgradeBought);

        unUpgradedGroup.SetActive(!GameManager.instance.runVariables.upgradeBought);
        upgradedGroup.SetActive(GameManager.instance.runVariables.upgradeBought);
    }

    public void DisableShop() => shopUI.SetActive(false);

    public void OnPlayerEvade(float cooldownTime)
    {
        StartCoroutine(CooldownAnimation(cooldownTime));
    }

    IEnumerator CooldownAnimation(float cooldownTime)
    {
        
        float timePassed = 0f;
        cooldownSlider.value = (1f - (timePassed / cooldownTime)) * GameManager.instance.cooldownPercent;

        yield return new WaitForSecondsRealtime(
            GameManager.instance.levelVariables.evadeType == EvadeType.Dash?
                GameManager.instance.playerConstants.dashTime:
                GameManager.instance.playerConstants.parryTime
        );

        while (timePassed < cooldownTime)
        {
            cooldownSlider.value = (1f - (timePassed / cooldownTime)) * GameManager.instance.cooldownPercent;
            timePassed += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    public void UpdateCooldownBlock(ConsumableType consumableType)
    {
        if (consumableType == ConsumableType.Adderall)
        {
            cooldownBlockSlider.value = 1f - GameManager.instance.cooldownPercent;
        }
    }

    public void UpdateCooldownSprite(EvadeType evadeType)
    {
        evadeIcon.sprite = (evadeType == EvadeType.Dash) ? GameManager.instance.playerConstants.dashIcon : GameManager.instance.playerConstants.parryIcon;
    }


}
