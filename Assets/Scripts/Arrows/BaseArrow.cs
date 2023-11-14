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
        Debug.Log("shoot!");
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - bossCoords.position;
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 45);

        GameManager.instance.PlayAudioElement(throwArrowAudioElement);
    }
}
