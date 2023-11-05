using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractables
{
    void OnInteract(SpriteRenderer spriteRenderer);
}
public enum InteractableType
{
    Interactable = 0,
    Holdable = 1,
    Receivable = 2,

    ToShred = 3,
    ToLaminate = 4,

}