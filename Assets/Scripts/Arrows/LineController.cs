using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform[] points = new Transform[2];
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void SetUpLine(Transform[] points)
    {
        lineRenderer.positionCount = points.Length;
        this.points = points;
    }

    public void turnOff(Transform bossCoords)
    {
        Destroy(gameObject);
    }
    public void turnOn(Transform bossCoords)
    {
        Transform playerCoords = GameObject.FindGameObjectWithTag("Player").transform;
        points[0] = playerCoords;
        points[1] = bossCoords;
        lineRenderer.enabled = true;
    }


    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, (i == 0) ? points[i].position + 100f * (points[i].position - points[i + 1].position) : points[i].position);
        }
    }
}
