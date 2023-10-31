using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Takes and handles input and movement for a player character
public class Interactable : MonoBehaviour, IInteractables
{

    public void OnInteract()
    {
        Debug.Log("Interacted with me!");
    }

}
