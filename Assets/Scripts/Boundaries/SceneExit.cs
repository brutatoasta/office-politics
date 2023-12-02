using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    private string[] scenes = { "Level 1", "Cutscene", "PowerUpScene", "Level 2", "Cutscene", "PowerUpScene", "Level 3", "Cutscene", "PowerUpScene", "Level 4", "Cutscene" };
    public LevelVariables levelVariables;
    public Animator transition;
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
    public GameObject controlsPanel;
    public GameObject shade;
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
    private string nextScene;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (collision.CompareTag("Player"))
        {
            // load next scene based on current scene

            // switch (scene.name)
            // {
            //     case "PowerUpScene":
            //         nextScene = "Map";
            //         // change some scriptable object values
            //         break;
            //     case "Map":
            //         // might need a transition scene for night to day 
            //         nextScene = "Cutscene";
            //         GameManager.instance.LevelStart();
            //         GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.levelComplete);
            //         // change some scriptable object values
            //         break;
            //     case "Cutscene":
            //         // might need a transition scene for night to day 
            //         nextScene = "PowerUpScene";
            //         GameManager.instance.LevelStart();
            //         // change some scriptable object values
            //         break;
            //     default:
            //         // no op
            //         Debug.Log("didnt change scene");
            //         break;
            // }
            // fade to black

            if (GameManager.instance.runVariables.currentSceneIndex < scenes.Length)
            {
                nextScene = scenes[GameManager.instance.runVariables.currentSceneIndex];
                if (nextScene == "Cutscene")
                {
                    GameManager.instance.levelVariables.ExitLevel();
                }

                GameManager.instance.runVariables.currentSceneIndex++;
                // fading in and out
                transition.SetTrigger("Start");
                // Start the coroutine to load the next scene after a delay
                StartCoroutine(LoadNextSceneAfterDelay(nextScene, 1f));
            }
            else
            {
                Debug.Log("All scenes loaded.");
            }
            switch (nextScene)
            {
                case "Level 1":
                    levelVariables.currentLevelIndex = 0;
                    GameManager.instance.LevelStart();
                    break;
                case "Level 2":
                    levelVariables.currentLevelIndex = 1;
                    GameManager.instance.LevelStart();
                    break;
                case "Level 3":
                    levelVariables.currentLevelIndex = 2;
                    GameManager.instance.LevelStart();
                    break;
                case "Level 4":
                    levelVariables.currentLevelIndex = 3;
                    GameManager.instance.LevelStart();
                    break;
                default:
                    break;
            }
        }
    }
}
