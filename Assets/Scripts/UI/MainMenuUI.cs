using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Animator transition;
    public GameObject controlsPanel;
    public GameObject mapPanel;
    public GameObject shade;
    public GameObject notifsButton;
    public void PlayGame()
    {
        GameManager.instance.LevelStart();
        // fading in and out
        transition.SetTrigger("Start");
        StartCoroutine(LoadNextSceneAfterDelay("PowerUpScene", 0.5f));

        // natthan - sfx for main menu click "start work"
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuClick);
    }
    IEnumerator LoadNextSceneAfterDelay(string nextScene, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Load the next scene after the delay
        SceneManager.LoadSceneAsync(nextScene);
    }
    public void QuitGame() => Application.Quit();

    public void Controls()
    {
        // Ensure the panel is initially at scale zero
        controlsPanel.transform.localScale = Vector3.zero;

        // Activate the panel
        controlsPanel.SetActive(true);

        // Start the opening animation
        controlsPanel.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
        shade.SetActive(true);

        // natthan - sfx for pause menu click "controls"
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuClick);
    }
    public void CloseControls()
    {
        controlsPanel.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo().setOnComplete(() =>
        {
            controlsPanel.SetActive(false);
        });
        shade.SetActive(false);
        notifsButton.SetActive(false);

        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuBack);
    }
    public void Map()
    {
        // Ensure the panel is initially at scale zero
        mapPanel.transform.localScale = Vector3.zero;

        // Activate the panel
        mapPanel.SetActive(true);

        // Start the opening animation
        mapPanel.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuClick);
    }
    public void CloseMap()
    {
        mapPanel.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo().setOnComplete(() =>
        {
            mapPanel.SetActive(false);
        });
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuBack);
    }
}
