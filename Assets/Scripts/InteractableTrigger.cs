using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class InteractableTrigger : MonoBehaviour
{
    private SpriteRenderer map;
    

    void Start()
    {
        map = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        map.color = Color.gray;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 7)
        {
            map.color = Color.white;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 7)
        {
            map.color = Color.gray;
        }
    }
}
