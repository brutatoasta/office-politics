using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineControllerTesting : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController lineController;
    // Start is called before the first frame update
    private void Start()
    {
        lineController.SetUpLine(points);
    }

}
