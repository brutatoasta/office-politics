using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StressArrow : BaseArrow
{
    public GameObject lineRenderer;
    // Start is called before the first frame update
    public UnityEvent<Transform> StartLineRenderer;
    public UnityEvent<Transform> StopLineRenderer;
    private float speed;

    private void Awake()
    {
        speed = weaponGameConstants.stressArrowSpeed;
    }
    IEnumerator SpawnArrowRoutine(Transform bossCoords)
    {
        Instantiate(lineRenderer,this.transform);
        StartLineRenderer.Invoke(bossCoords);
        yield return new WaitForSeconds(2f);
        StopLineRenderer.Invoke(bossCoords);
        yield return new WaitForSeconds(0.1f);
        Rigidbody2D arrowRigidBody = gameObject.AddComponent<Rigidbody2D>();
        arrowRigidBody.gravityScale = 0;
        base.Shoot(bossCoords,speed);
    }

    public void SpawnArrow(Transform bossCoords)
    {
        StartCoroutine(SpawnArrowRoutine(bossCoords));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
        {
            Debug.Log("Stress Arrow has hit " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}