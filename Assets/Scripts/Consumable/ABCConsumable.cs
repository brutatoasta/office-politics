using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class ABCConsumable
{
    public ConsumableData data;
    public void Consume()
    {
        if (data.count > 0)
        {
            ConsumeEffect();
            data.count--;
        }
        else
        {
            Debug.Log("Empty");
        }
    }
    public abstract void ConsumeEffect();
}
[Serializable]
public struct ConsumableData
{
    public int count;
    public Sprite sprite;
}