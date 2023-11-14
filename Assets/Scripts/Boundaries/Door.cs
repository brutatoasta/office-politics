using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent toggleBossBehaviour;
    private bool fromInside = false;
    private bool isOutside = true;
    public bool rightIsInside = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            toggleBossBehaviour.Invoke();
            GameManager.instance.StartTimer();
        }
    }

    private bool playerIsLeftOfDoor;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // implement xnor logic for sideways first
            playerIsLeftOfDoor = collision.transform.position.x - gameObject.transform.position.x < 0;
            isOutside = playerIsLeftOfDoor == rightIsInside;
            if (!fromInside)
            {
                if (isOutside)
                {
                    toggleBossBehaviour.Invoke();
                }
                else
                {
                    fromInside = !fromInside;
                }
            } else
            {
                if (!isOutside)
                {
                    toggleBossBehaviour.Invoke();
                }
                else
                {
                    fromInside = !fromInside;
                }
            }
        }
    }
}
