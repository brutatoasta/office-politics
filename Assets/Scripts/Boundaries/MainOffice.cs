using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainOffice : MonoBehaviour
{
    public UnityEvent toggleBossBehaviour;
    private bool isInRoom = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
