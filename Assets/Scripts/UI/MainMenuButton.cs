using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    public void MainMenu()
    {
        Time.timeScale = 1;
        GameManager.instance.levelVariables.currentLevelIndex = 0;
        GameManager.instance.ReturnToMainMenu();
    }
}
