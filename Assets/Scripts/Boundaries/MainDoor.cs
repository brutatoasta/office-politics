using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public GameObject sceneExit;
    public EdgeCollider2D edgeCollider2D;
    bool entryTrigger = true;

    void Start()
    {
        GameManager.instance.doorOpen.AddListener(OpenDoor);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (entryTrigger)
        {
            sceneExit.SetActive(true);
            GameManager.instance.StartTimer();
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.gameplayBgmIntensity1);
            entryTrigger = false;
        }
    }

    public void OpenDoor()
    {
        edgeCollider2D.enabled = false;
    }
}
