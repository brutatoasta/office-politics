using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    override  public  void  Awake(){
        base.Awake();
    }


    public UnityEvent<Interactable> interact;
}
