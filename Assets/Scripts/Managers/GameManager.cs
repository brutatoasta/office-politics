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

    // events
    public UnityEvent gameStart; // start the level (not pantry or hostel)
    public UnityEvent gameRestart; // go back to main menu
    public UnityEvent gamePause; // 
    public UnityEvent gamePlay; // 

    public UnityEvent gameOver;
    public UnityEvent doorOpen;

    // Inventory
    public UnityEvent<int> cycleInventory;
    public UnityEvent useConsumable;

    // Timer
    public UnityEvent TimerStart;
    public UnityEvent TimerStop;

    public UnityEvent<float> TimerUpdate;

    public UnityEvent increaseStress;
    public UnityEvent<int, Transform> increasePerformancePoint;
    public UnityEvent switchTasks;

    // Scriptable Objects
    public AudioElementGameEvent audioElementGameEvent;
    public PlayerConstants playerConstants;
    public InventoryVariable inventory;
    public bool isPaused = false;
    public bool overtime = false;
    private int currentInventorySlot = 0;

    public Sprite kitKatSprite;
    public Sprite coffeeSprite;

    public GameObject held;

    void Start()
    {
        // start of whole game, not level
        inventory.consumableObjects = new ABCConsumable[] { new KitKat(2, kitKatSprite), new Coffee(1, coffeeSprite), new KitKat(3, kitKatSprite) };
        inventory.stressPoint = 0;
    }

    // Raise event to cycle inventory slot
    public void CycleInventory()
    {
        // find next slot that contains an item
        for (int i = 0; i < inventory.consumableObjects.Length; i++)
        {
            currentInventorySlot = (currentInventorySlot + 1) % inventory.consumableObjects.Length;
            if (inventory.consumableObjects[currentInventorySlot].count != 0) break;
        }

        cycleInventory.Invoke(currentInventorySlot);
    }

    public void UseCurrentConsumable()
    {
        inventory.consumableObjects[currentInventorySlot].Consume();
        useConsumable.Invoke();
    }


    public void IncreaseStress()
    {
        if (inventory.stressPoint > 50) GameOver();
        increaseStress.Invoke();
    }

    public void PlayAudioElement(AudioElement audioElement)
    {
        audioElementGameEvent.Raise(audioElement);
    }
    public void PlayPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            // check if in a level
            gamePlay.Invoke();
            // hide pause menu
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            gamePause.Invoke();
            // show pause menu
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

    // Timer
    public void StartTimer() => TimerStart?.Invoke();
    public void OnStopTimer()
    {
        overtime = true;
        TimerStop?.Invoke();

        bool quotaComplete = true;
        foreach (TaskItem taskItem in inventory.taskQuotas)
        {
            if (taskItem.quota > 0)
            {
                quotaComplete = false;
                break;
            }
        }
        if (quotaComplete) doorOpen.Invoke();
    }
    public void UpdateTimer(float value) => TimerUpdate?.Invoke(value);


    public void DecreaseQuota()
    {
        if (overtime)
        {
            bool quotaComplete = true;
            foreach (TaskItem taskItem in inventory.taskQuotas)
            {
                if (taskItem.quota > 0)
                {
                    quotaComplete = false;
                    break;
                }
            }
            Debug.LogError(quotaComplete);
            if (quotaComplete) doorOpen.Invoke();
        }
    }
    void OnSceneLoaded()
    {
        // check which scene
        // if Map.scene, start timer after 3 seconds
        // invoke gameplay event?

    }
}
