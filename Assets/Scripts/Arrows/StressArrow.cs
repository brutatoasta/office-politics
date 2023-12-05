using Pathfinding;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StressArrow : BaseArrow
{
    public GameObject lineRenderer;

    public UnityEvent<Transform> StartLineRenderer;
    public UnityEvent<Transform> StopLineRenderer;
    private float speed;

    public override void OnParry() { }
    private TrailRenderer trailRenderer;
    private void Awake()
    {
        speed = weaponGameConstants.stressArrowSpeed;
        throwArrowAudioElement = GameManager.instance.audioElements.throwStressArrow;
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }
    IEnumerator SpawnArrowRoutine(Transform bossCoords)
    {
        Instantiate(lineRenderer, this.transform);
        StartLineRenderer.Invoke(bossCoords);
        yield return new WaitForSeconds(2f);
        StopLineRenderer.Invoke(bossCoords);
        yield return new WaitForSeconds(0.1f);
        trailRenderer.enabled = true;
        Rigidbody2D arrowRigidBody = gameObject.AddComponent<Rigidbody2D>();
        arrowRigidBody.gravityScale = 0;
        Shoot(bossCoords, speed);
    }

    public void SpawnArrow(Transform bossCoords)
    {
        StartCoroutine(SpawnArrowRoutine(bossCoords));
    }
}