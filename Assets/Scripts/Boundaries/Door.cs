using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent toggleBossBehaviour;
    private bool hasEntered = false;
    private bool isOutside = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            toggleBossBehaviour.Invoke();
            GameManager.instance.StartTimer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isOutside = collision.transform.position.x - gameObject.transform.position.x < 0;
            if (!hasEntered)
            {
                if (isOutside)
                {
                    toggleBossBehaviour.Invoke();
                }
                else
                {
                    hasEntered = true;
                }
            } else
            {
                if (!isOutside)
                {
                    toggleBossBehaviour.Invoke();
                }
                else
                {
                    hasEntered = false;
                }
            }
        }
    }
}
