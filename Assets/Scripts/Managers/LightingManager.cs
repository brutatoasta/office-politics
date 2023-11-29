using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingManager : MonoBehaviour
{
    public GameObject miniGlobal;
    void Start()
    {
        GameManager.instance.TimerStop.AddListener(TurnOffLights);
        miniGlobal.SetActive(true);
    }

    // Update is called once per frame
    public void TurnOffLights() => miniGlobal.SetActive(false);
}
