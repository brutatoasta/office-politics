using UnityEngine;

// controls player's entry and exit for that level 
public class Entrance : MonoBehaviour
{
    public GameObject sceneExit;
    public EdgeCollider2D edgeCollider2D;
    bool entryTrigger = true;
    bool firstCollision = true;

    void Start()
    {
        GameManager.instance.doorOpen.AddListener(OpenDoor);
        GameManager.instance.doorClose.AddListener(CloseDoor);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (entryTrigger)
        {
            sceneExit.SetActive(true);
            GameManager.instance.StartTimer();
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.gameplayBGMIntensity1);
            transform.GetChild(1).GetComponent<Animator>().SetBool("isDoorOpen", false);
            entryTrigger = false;

            // natthan - sfx for close door
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.closeDoor);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // natthan - deny exit sfx on collisions with the door after player enters the office.
        // player entering the office counts as a collision, hence the first collision should be ignored.
        if (col.gameObject.CompareTag("Player"))
        {
            if (firstCollision)
            {
                firstCollision = false;
            }
            else
            {
                GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.denyExit);
            }
        }
    }

    public void OpenDoor()
    {
        edgeCollider2D.enabled = false;
        transform.GetChild(1).GetComponent<Animator>().SetBool("isDoorOpen", true);
    }
    public void CloseDoor()
    {
        edgeCollider2D.enabled = true;
        transform.GetChild(1).GetComponent<Animator>().SetBool("isDoorOpen", false);
    }
}
