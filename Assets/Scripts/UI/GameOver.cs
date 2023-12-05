using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
