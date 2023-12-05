using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeEntrance : MonoBehaviour
{
    public bool quotaFilled = false;
    void Start()
    {
        GameManager.instance.doorOpen.AddListener(OpenDoor);
        GameManager.instance.doorClose.AddListener(CloseDoor);
    }

    void OpenDoor()
    {
        Debug.LogError("Quota");
        quotaFilled = true;
    }
    void CloseDoor()
    {
        quotaFilled = false;
    }

}
