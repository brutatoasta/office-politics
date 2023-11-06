using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StressArrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject lineRenderer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    public UnityEvent<Transform> StartLineRenderer;
    public UnityEvent<Transform> StopLineRenderer;

    IEnumerator SpawnArrowRoutine(Transform bossCoords)
    {
        Instantiate(lineRenderer,this.transform);
        StartLineRenderer.Invoke(bossCoords);
        yield return new WaitForSeconds(2f);
        StopLineRenderer.Invoke(bossCoords);
        yield return new WaitForSeconds(0.1f);
        Rigidbody2D arrowRigidBody = gameObject.AddComponent<Rigidbody2D>();
        arrowRigidBody.gravityScale = 0;
        Shoot(bossCoords, player.transform);
    }

    public void SpawnArrow(Transform bossCoords)
    {
        StartCoroutine(SpawnArrowRoutine(bossCoords));
    }

    public void Shoot(Transform bossCoords, Transform playerCoords) 
    { 
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - bossCoords.position;
        rb.velocity = new Vector2(direction.x, direction.y);

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 45);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
        {
            Debug.Log("Job Arrow has hit " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}