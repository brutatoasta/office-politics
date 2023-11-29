using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    private string[] scenes = { "Level 1", "Cutscene", "PowerUpScene", "Level 2", "Cutscene", "PowerUpScene", "Level 3", "Cutscene", "PowerUpScene", "Level 4", "Cutscene" };
    public LevelVariables levelVariables;
    public void PlayGame()
    {
        GameManager.instance.LevelStart();
        SceneManager.LoadSceneAsync("PowerUpScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public GameObject controlsPanel;
    public void Controls()
    {
        controlsPanel.SetActive(true);
    }
    public void CloseControls()
    {
        controlsPanel.SetActive(false);
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
            if (levelVariables.currentSceneIndex < scenes.Length)
            {
                nextScene = scenes[levelVariables.currentSceneIndex];
                levelVariables.currentSceneIndex++;
                SceneManager.LoadSceneAsync(nextScene);
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
