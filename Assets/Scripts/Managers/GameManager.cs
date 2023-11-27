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
    public UnityEvent runStart; // start the level (not pantry or hostel)
    public UnityEvent gameRestart; // go back to main menu
    public UnityEvent gamePause; // 
    public UnityEvent gamePlay; // 

    public UnityEvent levelStart;

    public UnityEvent interact;

    public UnityEvent gameOver;
    public UnityEvent doorOpen;

    // runVariables
    public UnityEvent<int> cycleInventory;
    public UnityEvent useConsumable;

    // Timer
    public UnityEvent TimerStart;
    public UnityEvent TimerStop;

    public UnityEvent<float> TimerUpdate;

    // scoring and tasks system
    public UnityEvent increaseStress;
    // public UnityEvent<int, Transform> increasePerformancePoint;
    public UnityEvent onTaskSuccess;
    public UnityEvent showPerformancePoint;
    public UnityEvent heldSet;

    // Scriptable Objects
    public AudioElementGameEvent audioElementGameEvent;
    public PlayerConstants playerConstants;
    public TaskConstants taskConstants;
    public RunVariables runVariables;
    public LevelVariables levelVariables;
    public AudioElements audioElements;
    public bool isPaused = false;
    public bool overtime = false;
    private int currentInventorySlot = 0;

    public Sprite kitKatSprite;
    public Sprite coffeeSprite;

    public GameObject held;

    private int currentLevel = 0;
    void Start()
    {
        levelVariables.Init(currentLevel);
        runVariables.Init();
    }

    public void RunStart()
    {
        runVariables.Init();
        runStart.Invoke();
    }

    public void LevelStart()
    {
        Debug.Log("InitCalled");
        levelStart.Invoke();
        UpdateTimer(120);
        levelVariables.Init(0);
    }

    // Raise event to cycle runVariables slot
    public void CycleInventory()
    {
        // find next slot that contains an item
        for (int i = 0; i < runVariables.consumableObjects.Length; i++)
        {
            currentInventorySlot = (currentInventorySlot + 1) % runVariables.consumableObjects.Length;
            if (runVariables.consumableObjects[currentInventorySlot].count != 0) break;
        }

        cycleInventory.Invoke(currentInventorySlot);
    }

    public void UseCurrentConsumable()
    {
        runVariables.consumableObjects[currentInventorySlot].Consume();
        useConsumable.Invoke();
    }

    public void IncreaseJob()
    {
        levelVariables.addRandomJob(currentLevel);
    }
    public void IncreaseStress()
    {
        if (levelVariables.stressPoints >= levelVariables.maxStressPoints) GameOver();
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
        gameRestart.Invoke();
        Time.timeScale = 1;

        isPaused = false;
        SceneManager.LoadSceneAsync("MainMenu");
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


        if (levelVariables.isQuotaComplete()) doorOpen.Invoke();
    }
    public void UpdateTimer(float value) => TimerUpdate?.Invoke(value);


    public void DecreaseQuota()
    {
        if (overtime && levelVariables.isQuotaComplete()) DoorOpen();
    }
    
    public void DoorOpen()
    {
        doorOpen.Invoke();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void SetHeld(GameObject newHeld)
    {
        held = newHeld;
        heldSet.Invoke();
    }

    void OnSceneLoaded()
    {
        // check which scene
        // if Map.scene, start timer after 3 seconds
        // invoke gameplay event?

    }
}
