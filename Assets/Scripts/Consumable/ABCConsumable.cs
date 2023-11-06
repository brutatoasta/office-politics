using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABCConsumable
{
    public int count;
    public Sprite sprite;

    public void Consume() {
        if (count > 0)
        {
            ConsumeEffect();
            count--;
        }
        else
        {
            Debug.Log("Empty");
        }
    }
    public abstract void ConsumeEffect();
}
