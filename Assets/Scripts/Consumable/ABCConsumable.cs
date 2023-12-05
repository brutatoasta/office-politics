using System;
using UnityEngine;
[Serializable]
public abstract class ABCConsumable
{
    public int count;
    public int cost;
    public Sprite sprite;

    // Decrease ammount and apply effect
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

    // Dynamic loading of consumable sprite
    public void LoadSprite(string resourcePath)
    {
        sprite = Resources.Load<Sprite>(resourcePath);

        if (sprite == null)
        {
            Debug.LogError("Failed to load KitKat sprite!");
        }
    }

    // Abstract method for child effect
    public abstract void ConsumeEffect();
}
