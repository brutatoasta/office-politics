using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    override public void Awake()
    {
        base.Awake();
    }
    #region Variables
    // events
    // public UnityEvent runStart; // start the level (not pantry or hostel)
    public UnityEvent gameRestart; // go back to main menu
    public UnityEvent levelStart;
    public UnityEvent interact;
    public UnityEvent gameOver;
    public UnityEvent doorOpen;
    public UnityEvent doorClose;

    // runVariables
    public UnityEvent updateInventory;
    public UnityEvent<int> useConsumable;
    public UnityEvent<ConsumableType> consumableEfffect;

    // Timer
    public UnityEvent TimerStart;
    public UnityEvent TimerStop;

    public UnityEvent<float> TimerUpdate;

    // scoring and tasks system
    public UnityEvent increaseStress;
    public UnityEvent onTaskSuccess;
    public UnityEvent showPerformancePoint;
    public UnityEvent<int> animatePerformancePoint;
    public UnityEvent<TaskItem> animateTaskAdd;
    public UnityEvent heldSet;
    // UI
    public UnityEvent<EvadeType> updateEvade;
    public UnityEvent<float> playerEvade;
    public UnityEvent playerFreeze;
    public UnityEvent playerUnFreeze;

    // Scriptable Objects
    public AudioElementGameEvent audioElementGameEvent;
    public PlayerConstants playerConstants;
    public TaskConstants taskConstants;
    public RunVariables runVariables;
    public LevelVariables levelVariables;
    public EndingVariables endingVariables;
    public AudioElements audioElements;
    public bool isPaused = false;
    public bool overtime = false;
    [NonSerialized] public bool invincible = false;
    public float cooldownPercent = 1f;
    private int currentInventorySlot = 0;
    [NonSerialized] public List<int> activeSlots = new List<int> { 0, 1 };

    public Sprite kitKatSprite;
    public Sprite coffeeSprite;
    public GameObject held;
    #endregion

    void Start()
    {
        levelVariables.Init();
        runVariables.currentSceneIndex = 0;
        levelVariables.currentLevelIndex = 0;
        levelVariables.levelPP = 0;
        runVariables.Init();
        endingVariables.Init();
        Debug.Log(string.Join(",", activeSlots));

        gameRestart.AddListener(LevelStart);

        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => CycleInventory();
    }

    // Called Before Each Level
    public void LevelStart()
    {
        Debug.Log("InitCalled");
        //levelStart.Invoke();
        UpdateTimer(runVariables.duration);
        levelVariables.Init();

    }

    // Raise event to cycle runVariables slot
    public void CycleInventory()
    {
        // find start slot
        for (int i = 0; i < runVariables.consumableObjects.Length; i++)
        {
            currentInventorySlot = (currentInventorySlot + 1) % runVariables.consumableObjects.Length;
            if (runVariables.consumableObjects[currentInventorySlot].count != 0) break;
        }

        int currIdx = currentInventorySlot;
        List<int> res = new List<int>();

        while (res.Count < activeSlots.Count)
        {
            if (runVariables.consumableObjects[currIdx].count > 0)
            {
                res.Add(currIdx);
            }

            currIdx = (currIdx + 1) % runVariables.consumableObjects.Length;

            if (currIdx == currentInventorySlot) break;
        }
        while (res.Count < (runVariables.upgradeBought ? 3 : 2))
        {
            res.Add(-1);
        }

        activeSlots = res;
        updateInventory.Invoke();
    }

    // Use consumable in given slot
    public void UseCurrentConsumable(int slot)
    {

        if (runVariables.upgradeBought && activeSlots[slot] != -1)
        {
            runVariables.consumableObjects[activeSlots[slot]].Consume();

            // sfx for use consumable
            PlayAudioElement(audioElements.useConsumable);
        }
        else if (!runVariables.upgradeBought && activeSlots[0] != -1)
        {
            runVariables.consumableObjects[activeSlots[0]].Consume();

            // sfx for use consumable
            PlayAudioElement(audioElements.useConsumable);
        }
        else
        {
            // sfx for cannot use consumable
            PlayAudioElement(audioElements.cannotPurchaseItem);
        }

        // Blank cleaned slots
        if (activeSlots[runVariables.upgradeBought ? slot : 0] != -1 &&
         runVariables.consumableObjects[activeSlots[runVariables.upgradeBought ? slot : 0]].count <= 0)
        {
            activeSlots[runVariables.upgradeBought ? slot : 0] = -1;
        }
        useConsumable.Invoke(slot);
    }

    // Hit by Job arrow
    public void IncreaseJob()
    {
        levelVariables.AddRandomJob(levelVariables.currentLevelIndex);
        doorClose.Invoke();

        // sfx for increase task quota
        PlayAudioElement(audioElements.increaseTaskQuota);
    }

    // Tutorial Job arrow
    public void IncreaseCoffeeJob()
    {
        levelVariables.AddCoffeeJob();
    }

    // General method to increase stress & invoke event to notify
    public void IncreaseStress()
    {
        if (levelVariables.stressPoints >= levelVariables.maxStressPoints) GameOver();
        increaseStress.Invoke();
    }

    // Function to raise event that is listened to by audio manager
    public void PlayAudioElement(AudioElement audioElement)
    {
        audioElementGameEvent.Raise(audioElement);
    }

    // Toggle pause menu
    public void PlayPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            PlayAudioElement(audioElements.gameResume);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            PlayAudioElement(audioElements.gamePause);
        }
    }

    // Restart the game after a run
    public void GameRestart()
    {
        gameRestart.Invoke();
        Time.timeScale = 1;

        isPaused = false;
        runVariables.Init();
        levelVariables.Init();
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void GameOver()
    {
        // goto badending
        gameOver.Invoke();
    }


    // Timer
    public void StartTimer() => TimerStart?.Invoke();
    public void OnStopTimer()
    {
        overtime = true;
        TimerStop?.Invoke();
        // natthan - sfx for overtime start
        PlayAudioElement(audioElements.overtimeStart);
    }
    public void UpdateTimer(float value) => TimerUpdate?.Invoke(value);

    // Check whenever task is completed, open door if quota complete
    public void DecreaseQuota()
    {
        if (levelVariables.IsQuotaComplete()) DoorOpen();
    }

    public void DoorOpen()
    {
        doorOpen.Invoke();
    }

    // Notify whenever player held object changes
    public void SetHeld(GameObject newHeld)
    {
        held = newHeld;
        heldSet.Invoke();
    }

    // Notify whenever evade type is changed
    public void UpdateEvadeType(EvadeType evadeType)
    {
        levelVariables.evadeType = evadeType;
        updateEvade.Invoke(evadeType);
    }
}
