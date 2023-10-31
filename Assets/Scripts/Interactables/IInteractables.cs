using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractables
{
    public void OnInteract();
}
public enum InteractableType
{
    Interactable = 0,
    Holdable = 1

}