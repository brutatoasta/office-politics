using UnityEngine;

public class StressArrow : MonoBehaviour
{
    public Rigidbody2D rb;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y);

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Arrow has hit " + collision.gameObject.name);
        Destroy(gameObject);
    }
}