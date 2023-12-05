using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineControllerTesting : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController lineController;
    private float timer;
    public UnityEvent launchArrow;

    private void Start()
    {
    }
    void Update()
    {
        timer += Time.deltaTime;
        //if (timer > 2)
        //{
        //    lineController.SetUpLine(points);
        //    lineController.turnOn();
        //}
        //if (timer > 4)
        //{
        //    lineController.turnOff();
        //}
        //if (timer > 4.1)
        //{
        //    launchArrow.Invoke();
        //    timer = -2;
        //}
    }

}
