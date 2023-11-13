using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public InventoryVariable inventory;
    public Image current;
    public CanvasGroup currentGroup;
    private int currSlot;
    public Image next;
    public CanvasGroup nextGroup;
    private int nextSlot;
    public CanvasGroup fade;
    public GameObject countText;
    public Slider slider;
    public PlayerConstants playerConstants;
    public GameObject performancePointText;

    void Start()
    {
        GameManager.instance.cycleInventory.AddListener(CycleInventory);
        GameManager.instance.useConsumable.AddListener(UseConsumable);
        GameManager.instance.increaseStress.AddListener(StressBarSlider);
        GameManager.instance.increasePerformancePoint.AddListener(PerformancePoint);


        current.sprite = inventory.consumableObjects[0].sprite;
        next.sprite = inventory.consumableObjects[1].sprite;

        currSlot = 0;
        nextSlot = 1;

        countText.GetComponent<TextMeshProUGUI>().text = "" + inventory.consumableObjects[0].count;
    }

    public void CycleInventory(int currentInventorySlot)
    {
        current.sprite = (inventory.consumableObjects[currentInventorySlot].count > 0) ?
                            inventory.consumableObjects[currentInventorySlot].sprite :
                            null;

        int nextInventorySlot = (currentInventorySlot + 1) % inventory.consumableObjects.Length;
        for (int i = 0; i < inventory.consumableObjects.Length; i++)
        {
            if (inventory.consumableObjects[nextInventorySlot].count != 0) break;
            nextInventorySlot = (nextInventorySlot + 1) % inventory.consumableObjects.Length;
        }
        next.sprite = (inventory.consumableObjects[nextInventorySlot].count > 0 && nextInventorySlot != currentInventorySlot) ?
                            inventory.consumableObjects[nextInventorySlot].sprite :
                            null;

        currSlot = currentInventorySlot;
        nextSlot = nextInventorySlot;

        currentGroup.alpha = (current.sprite == null) ? 0 : 1;
        nextGroup.alpha = (next.sprite == null) ? 0 : 1;

        countText.GetComponent<TextMeshProUGUI>().text = "" + inventory.consumableObjects[currentInventorySlot].count;
    }

    public void UseConsumable()
    {
        current.sprite = (inventory.consumableObjects[currSlot].count > 0) ?
                            inventory.consumableObjects[currSlot].sprite :
                            null;
        next.sprite = (inventory.consumableObjects[nextSlot].count > 0 && nextSlot != currSlot) ?
                            inventory.consumableObjects[nextSlot].sprite :
                            null;

        currentGroup.alpha = (current.sprite == null) ? 0 : 1;
        nextGroup.alpha = (next.sprite == null) ? 0 : 1;
        StartCoroutine(Fade());

        countText.GetComponent<TextMeshProUGUI>().text = "" + inventory.consumableObjects[currSlot].count;
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
        slider.value = playerConstants.stressPoint;
    }
    public void PerformancePoint()
    {
        performancePointText.GetComponent<TextMeshProUGUI>().text = "Performance Point: " + playerConstants.performancePoint;
    }

}
