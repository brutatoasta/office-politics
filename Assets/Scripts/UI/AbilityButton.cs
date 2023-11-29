using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public GameObject outline;
    public bool isDash;
    void Start()
    {
        // bgImg = GetComponent<Image>();
        UpdateBgColor(GameManager.instance.levelVariables.evadeType);
        GameManager.instance.updateEvade.AddListener(UpdateBgColor);
    }

    public void UpdateBgColor(EvadeType evadeType)
    {
        outline.SetActive(isDash? evadeType == EvadeType.Dash: evadeType == EvadeType.Parry);
    }
}
