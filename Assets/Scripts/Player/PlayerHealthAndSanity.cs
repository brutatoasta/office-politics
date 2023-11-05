using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthAndSanity : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerConstants playerConstants;
    public GameObject stressPointText;
    public Slider slider;
    void Start()
    {
        playerConstants.stressPoint = 0;
        slider.maxValue = playerConstants.maxStressPoint;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Arrow"))
        {
            playerConstants.stressPoint += 5;
            stressPointText.GetComponent<TextMeshProUGUI>().text = "Stress Point: " + playerConstants.stressPoint
        .ToString();
            slider.value = playerConstants.stressPoint;

        }
    }

}
