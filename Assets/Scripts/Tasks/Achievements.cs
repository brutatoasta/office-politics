using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Image NinjaImage;
    public UnityEngine.UI.Image SlackerImage;
    public UnityEngine.UI.Image SustainableWarriorImage;
    public UnityEngine.UI.Image BigEarnerImage;
    public UnityEngine.UI.Image WorkaholicImage;
    public UnityEngine.UI.Image TaichiMasterImage;
    public UnityEngine.UI.Image PerfectionistImage;
    public UnityEngine.UI.Image ReincarnationImage;
    public EndingVariables endingVariables;
    public TextMeshProUGUI positionText;

    private Dictionary<UnityEngine.UI.Image, bool> imageVariableMap = new Dictionary<UnityEngine.UI.Image, bool>();
    void Start()
    {
        AddToMap();

    }
    void Update()
    {
        if (endingVariables.firstTimeGame)
        {
            foreach (var i in imageVariableMap)
            {
                SetOpacity(i.Key, 0.5f);
                Debug.Log("test");
            }
            positionText.text = "Last Job Role:";
        }
        else
        {
            UpdateAchievements();
            positionText.text = "Last Job Role: " + endingVariables.GetPosition();
        }

    }

    // Update is called once per frame
    void AddToMap()
    {
        imageVariableMap.Add(SlackerImage, endingVariables.Slacker);
        imageVariableMap.Add(NinjaImage, endingVariables.Ninja);
        imageVariableMap.Add(SustainableWarriorImage, endingVariables.SustainableWarrior);
        imageVariableMap.Add(BigEarnerImage, endingVariables.BigEarner);
        imageVariableMap.Add(WorkaholicImage, endingVariables.Workaholic);
        imageVariableMap.Add(TaichiMasterImage, endingVariables.TaichiMaster);
        imageVariableMap.Add(PerfectionistImage, endingVariables.Perfectionist);
        imageVariableMap.Add(ReincarnationImage, endingVariables.Reincarnation);
    }

    void SetOpacity(UnityEngine.UI.Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
    void UpdateAchievements()
    {
        Debug.Log("test");
        foreach (var i in imageVariableMap)
        {
            SetOpacity(i.Key, i.Value ? 1.0f : 0.5f);
        }
    }
}
