using UnityEngine;

public class JobArrow : BaseArrow
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
        {
            Debug.Log("Job Arrow has hit " + collision.gameObject.name);
            Destroy(gameObject);

        }
    }
}