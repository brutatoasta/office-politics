using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    override public void Awake()
    {
        base.Awake();
    }

    //events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent gamePause;
    public UnityEvent gamePlay;
    public UnityEvent gameOver;


    public UnityEvent TimerStart;
    public UnityEvent TimerStop;
    public UnityEvent<float> TimerUpdate;


    public bool isPaused = false;
    public InventoryVariable invent;
    private int currentInventorySlot = 0;

    public Sprite kitKatSprite;
    public Sprite coffeeSprite;



    void Start()
    {
        invent.consumableObjects = new ABCConsumable[] { new KitKat(2, kitKatSprite), new Coffee(1, coffeeSprite), new KitKat(3, kitKatSprite) };
    }


    public UnityEvent<int> cycleInventory;
    public UnityEvent useConsumable;

    // Raise event to cycle inventory slot
    public void CycleInventory()
    {
        // find next slot that contains an item
        for (int i = 0; i < invent.consumableObjects.Length; i++)
        {
            currentInventorySlot = (currentInventorySlot + 1) % invent.consumableObjects.Length;
            if (invent.consumableObjects[currentInventorySlot].count != 0) break;
        }

        cycleInventory.Invoke(currentInventorySlot);
    }

    public void UseCurrentConsumable()
    {
        invent.consumableObjects[currentInventorySlot].Consume();
        useConsumable.Invoke();
    }
    public GameObject held;
    public UnityEvent increaseStress;

    public void PlayPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            gamePlay.Invoke();
            // hide menu
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            gamePause.Invoke();
            // show menu
        }
    }
    public void GameRestart()
    {
        // reset score

        gameRestart.Invoke();
        Time.timeScale = 1;

        isPaused = false;
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.Invoke();
    }

    public void StartTimer() => TimerStart.Invoke();
    public void StopTimer() => TimerStop.Invoke();
    public void UpdateTimer(float value) => TimerUpdate.Invoke(value);
}
