using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    //private Transform[] points;
    private Transform[] points = new Transform[2];

    //public UnityEvent launchArrow;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void SetUpLine(Transform[] points)
    {
        lineRenderer.positionCount = points.Length;
        this.points = points;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void turnOff(Transform bossCoords)
    {
        Destroy(gameObject);
    }
    public void turnOn(Transform bossCoords)
    {
        Transform playerCoords = GameObject.FindGameObjectWithTag("Player").transform;
        this.points[0] = playerCoords;
        this.points[1] = bossCoords;
        lineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
}
