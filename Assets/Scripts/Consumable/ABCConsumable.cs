using System;
using UnityEngine;
[Serializable]
public abstract class ABCConsumable
{
    // public ConsumableData data;
    public int count;
    public int cost;
    public Sprite sprite;
    public void Consume()
    {
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
    public void LoadSprite(string resourcePath)
    {
        sprite = Resources.Load<Sprite>(resourcePath);

        if (sprite == null)
        {
            Debug.LogError("Failed to load KitKat sprite!");
        }
    }
    public abstract void ConsumeEffect();
}
