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

    public bool isTopDown;
    public bool upIsInside = true;
    public bool rightIsInside = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            toggleBossBehaviour.Invoke();
            // GameManager.instance.StartTimer();
        }
    }

    private bool playerIsLeftOfDoor;
    private bool playerIsDownOfDoor;
    private bool evaluatePlayerSide(Collider2D collision)
    {
        if (isTopDown)
        {
            playerIsDownOfDoor = collision.transform.position.y - gameObject.transform.position.y < 0;
            return playerIsDownOfDoor == upIsInside;
        } else
        {
            playerIsLeftOfDoor = collision.transform.position.x - gameObject.transform.position.x < 0;
            return playerIsLeftOfDoor == rightIsInside;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // implement xnor logic for sideways first
            
            isOutside = evaluatePlayerSide(collision);
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
