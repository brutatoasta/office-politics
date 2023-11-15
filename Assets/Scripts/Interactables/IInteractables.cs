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
    ToFetchCoffee = 5,
    ToRefillCoffee = 6,
    ToFetchTea = 7,
    ToFetchDoc = 8,
    ToDeliverDoc = 9,
    ToChargeMic = 10,
    ToPrepMeeting = 11,
    ToPrepRefreshment = 12,
    ToReturnDoc = 13,
}
