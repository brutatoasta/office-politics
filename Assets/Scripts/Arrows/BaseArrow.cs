using System.Collections;
using UnityEngine;

public abstract class BaseArrow : MonoBehaviour, IArrow
{
    public ArrowTypes type;
    public WeaponGameConstants weaponGameConstants;
    public AudioElement throwArrowAudioElement;

    public Rigidbody2D rb;
    [System.NonSerialized]
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Shoot(Transform bossCoords, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - bossCoords.position;
        Vector2 direction_norm = (Vector2)direction.normalized;
        rb.velocity = direction_norm * speed;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 45);

        GameManager.instance.PlayAudioElement(throwArrowAudioElement);
    }

    public abstract void OnParry();

    private bool arrowHasLeftBoss = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
        {
            Debug.Log(gameObject.name + " has hit " + collision.gameObject.name);
            if (collision.gameObject.name == "Wall")
            {
                StartCoroutine(CollisionEffect());
            }
            else
            {
                Destroy(gameObject);
            }

        }
        else
        {
            if (arrowHasLeftBoss)
            {
                Enemy EnemyComponent = collision.GetComponent<Enemy>();
                EnemyComponent.stunByArrow();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator CollisionEffect()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        arrowHasLeftBoss = true;
    }
}
