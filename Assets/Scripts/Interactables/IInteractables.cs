using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractables
{
    void OnInteract();
}
public enum InteractableType
{
    Interactable,
    Holdable,
    Receivable,

    ToShred,
    ToLaminate,
    ToFetchCoffee,
    ToRefillCoffee,
    ToFetchTea,
    ToFetchDoc,
    ToDeliverDoc,
    ToChargeMic,
    ToPrepMeeting,
    ToPrepRefreshment,
    ToReturnDoc,
}

public interface IInteractableApplicable
{
    public Type RequestInteractable();
}
