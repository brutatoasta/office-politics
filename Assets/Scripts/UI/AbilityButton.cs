using UnityEngine;

public class AbilityButton : MonoBehaviour
{
    public GameObject outline;
    public bool isDash;
    void Start()
    {
        UpdateBgColor(GameManager.instance.levelVariables.evadeType);
        GameManager.instance.updateEvade.AddListener(UpdateBgColor);
    }

    public void UpdateBgColor(EvadeType evadeType)
    {
        outline.SetActive(isDash? evadeType == EvadeType.Dash: evadeType == EvadeType.Parry);
    }
}
