using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeEntrance : MonoBehaviour
{
    public bool quotaFilled = false;
    // Start is called before the first frame update
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

    // public void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (quotaFilled) GameManager.instance.GameOver();
    // }
}
