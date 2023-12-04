using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    #region Variables

    // private string[] scenes = { "Level 1", "Cutscene", "PowerUpScene", "Level 2", "Cutscene", "PowerUpScene", "Level 3", "Cutscene", "PowerUpScene", "Level 4", "Cutscene" };
    public LevelVariables levelVariables;
    public Animator transition;
    public GameObject controlsPanel;
    public GameObject shade;
    Scene currentScene;
    private string nextScene;
    #endregion
    public void PlayGame()
    {
        GameManager.instance.LevelStart();
        // fading in and out
        transition.SetTrigger("Start");
        StartCoroutine(LoadNextSceneAfterDelay("PowerUpScene", 0.5f));

        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuClick);
    }
    public void QuitGame()
    {
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuBack);
        Application.Quit();
    }

    public void Controls()
    {
        // Ensure the panel is initially at scale zero
        controlsPanel.transform.localScale = Vector3.zero;

        // Activate the panel
        controlsPanel.SetActive(true);

        // Start the opening animation
        controlsPanel.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
        shade.SetActive(true);

        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuClick);
    }
    public void CloseControls()
    {
        controlsPanel.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo().setOnComplete(() =>
        {
            controlsPanel.SetActive(false);
        });
        shade.SetActive(false);

        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuBack);
    }
    IEnumerator LoadNextSceneAfterDelay(string nextScene, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Load the next scene after the delay
        SceneManager.LoadSceneAsync(nextScene);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        currentScene = SceneManager.GetActiveScene();
        if (collision.CompareTag("Player"))
        {
            // load next scene based on current scene
            if (currentScene.name == SceneNames.Cutscene)
            {
                nextScene = SceneNames.PowerUpScene;
                levelVariables.currentLevelIndex++; // since we've left the level, we can increment the currentLevelIndex
            }
            else if (currentScene.name == SceneNames.PowerUpScene)
            {   // change to some level
                nextScene = SceneNames.Levels[levelVariables.currentLevelIndex];
                GameManager.instance.LevelStart();
            }
            else if (SceneNames.Levels.Contains(currentScene.name))
            {

                if (GameManager.instance.overtime)
                {
                    GameManager.instance.endingVariables.Slacker = false;
                    GameManager.instance.endingVariables.OTCount++;
                }
                GameManager.instance.levelVariables.ExitLevel();
                if (currentScene.name == SceneNames.Level4)
                {
                    nextScene = SceneNames.GoodEnding;
                }
                else
                {
                    // its currently either level 1, 2, or 3.
                    // load cutscene
                    nextScene = SceneNames.Cutscene;
                }

            }
            // fading in and out
            transition.SetTrigger("Start");
            // Start the coroutine to load the next scene after a delay
            StartCoroutine(LoadNextSceneAfterDelay(nextScene, 1f));

        }
    }
    public void StartBadEnding()
    {
        transition.SetTrigger("Start");
        // Start the coroutine to load the next scene after a delay
        StartCoroutine(LoadNextSceneAfterDelay(SceneNames.BadEnding, 1f));
    }
    void Awake()
    {
        GameManager.instance.gameOver.AddListener(StartBadEnding);
    }
}


public static class SceneNames
{
    public static readonly string Level1 = "Level 1";
    public static readonly string Level2 = "Level 2";
    public static readonly string Level3 = "Level 3";
    public static readonly string Level4 = "Level 4";
    public static readonly string Cutscene = "Cutscene";
    public static readonly string PowerUpScene = "PowerUpScene";
    public static readonly string GoodEnding = "GoodEnding";
    public static readonly string BadEnding = "GameOver";
    public static readonly List<string> Levels = new() { Level1, Level2, Level3, Level4 };
}