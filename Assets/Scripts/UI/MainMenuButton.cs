using UnityEngine;
public class MainMenuButton : MonoBehaviour
{
    public void MainMenu()
    {
        // sfx for main menu button click
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.menuClick);

        Time.timeScale = 1;
        GameManager.instance.levelVariables.currentLevelIndex = 0;
        GameManager.instance.GameRestart();
    }
}
