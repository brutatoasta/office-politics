using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainOffice : MonoBehaviour
{
    public UnityEvent toggleBossBehaviour;
    private bool isInRoom = false;

    void Start()
    {

    }


    void Update()
    {

    }

    public void toggleBossState(bool playerIsInsideRoom)
    {
        if (playerIsInsideRoom != isInRoom)
        {
            toggleBossBehaviour.Invoke();
            isInRoom = !isInRoom;
        }
    }
}
