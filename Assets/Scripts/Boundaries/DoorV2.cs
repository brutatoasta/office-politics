using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorV2 : MonoBehaviour
{
    public bool isTopDown;
    public bool isTopSideDoor;
    public bool isRightSideDoor;
    public UnityEvent<bool> NotifyRoom;

    private bool playerIsInsideRoom;
    private bool IsPlayerInsideRoom(Collider2D collision)
    {
        if (isTopDown)
        {
            bool playerIsDownOfDoor = collision.transform.position.y - gameObject.transform.position.y < 0;
            return playerIsDownOfDoor == isTopSideDoor;
        }
        else
        {
            bool playerIsLeftOfDoor = collision.transform.position.x - gameObject.transform.position.x < 0;
            return playerIsLeftOfDoor == isRightSideDoor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsInsideRoom = IsPlayerInsideRoom(collision);
            NotifyRoom.Invoke(playerIsInsideRoom);
        }
    }
}