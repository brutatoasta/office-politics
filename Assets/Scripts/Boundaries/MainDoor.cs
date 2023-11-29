using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public GameObject sceneExit;
    public EdgeCollider2D edgeCollider2D;

    void Start()
    {
        GameManager.instance.doorOpen.AddListener(OpenDoor);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        sceneExit.SetActive(true);
        GameManager.instance.StartTimer();

        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.gameplayBgmIntensity1);
    }

    public void OpenDoor()
    {
        edgeCollider2D.enabled = false;
    }
}
