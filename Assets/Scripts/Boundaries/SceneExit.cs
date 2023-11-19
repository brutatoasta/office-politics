using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneExit : MonoBehaviour
{
    public void PlayGame()
    {
        GameManager.instance.LevelStart();
        SceneManager.LoadSceneAsync("PowerUpScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private string nextScene;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (collision.CompareTag("Player"))
        {
            // load next scene based on current scene

            switch (scene.name)
            {
                case "PowerUpScene":
                    nextScene = "Map";
                    // change some scriptable object values
                    break;
                case "Map":
                    // might need a transition scene for night to day 
                    nextScene = "PowerUpScene";
                    GameManager.instance.LevelStart();
                    // change some scriptable object values
                    break;
                default:
                    // no op
                    Debug.Log("didnt change scene");
                    break;
            }
            // fade to black
            
            SceneManager.LoadSceneAsync(nextScene);
        }
    }
}
