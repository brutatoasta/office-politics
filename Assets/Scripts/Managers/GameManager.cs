using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    override  public  void  Awake(){
        base.Awake();
    }

    public InventoryVariable invent;
    private int currentInventorySlot = 0;

    public Sprite kitKatSprite;
    public Sprite coffeeSprite;

    void Start()
    {
        invent.consumableObjects = new ABCConsumable[] {new KitKat(2, kitKatSprite), new Coffee(1, coffeeSprite), new KitKat(3, kitKatSprite)};
    }


    public UnityEvent<int> cycleInventory;
    public UnityEvent useConsumable;

    // Raise event to cycle inventory slot
    public void CycleInventory ()
    {
        // find next slot that contains an item
        for (int i = 0; i < invent.consumableObjects.Length; i++)
        {
            currentInventorySlot = (currentInventorySlot+1) % invent.consumableObjects.Length;
            if (invent.consumableObjects[currentInventorySlot].count != 0) break;
        }

        cycleInventory.Invoke(currentInventorySlot);
    }

    public void UseCurrentConsumable()
    {
        invent.consumableObjects[currentInventorySlot].Consume();
        useConsumable.Invoke();
    }


}
