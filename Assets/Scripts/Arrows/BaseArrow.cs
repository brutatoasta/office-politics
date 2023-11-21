using UnityEngine;
using UnityEngine.Rendering;
public abstract class BaseArrow : MonoBehaviour, IArrow
{
    public ArrowTypes type;
    public WeaponGameConstants weaponGameConstants;
    public AudioElements audioElements;
    public AudioElement throwArrowAudioElement;

    public Rigidbody2D rb;
    [System.NonSerialized]
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Shoot(Transform bossCoords, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - bossCoords.position;
        Vector2 direction_norm = (Vector2) direction.normalized;
        rb.velocity = direction_norm * speed;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 45);

        GameManager.instance.PlayAudioElement(throwArrowAudioElement);
    }

    public abstract void OnParry();

    private bool arrowHasLeftBoss = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
        {
            Debug.Log(gameObject.name + " has hit " + collision.gameObject.name);
            Destroy(gameObject);

        }
        else
        {
            if (arrowHasLeftBoss)
            {
                Enemy EnemyComponent = collision.GetComponent<Enemy>();
                EnemyComponent.stunByArrow(); //should probably be a Unity Event since referencing another script hmmm TODO tmr i need to sleep now!!
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        arrowHasLeftBoss = true;
    }
}
