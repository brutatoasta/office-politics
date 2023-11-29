using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsTutorial : MonoBehaviour
{
    bool isActive = false;
    public void toggleActive()
    {
        isActive = !isActive;
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }
}

