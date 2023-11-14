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
    }

    void OpenDoor()
    {
        Debug.LogError("Quota");
        quotaFilled = true;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (quotaFilled) GameManager.instance.GameOver();
    }
}
