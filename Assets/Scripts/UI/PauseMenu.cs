using UnityEngine;

public class PauseMenu : MonoBehaviour
{


    bool isPaused = false;
    void Awake()
    {
        gameObject.SetActive(isPaused);
    }

    public void toggleOnState()
    {
        isPaused = !isPaused;
        gameObject.SetActive(isPaused);
        GameManager.instance.PlayPause();
    }
    public void OnResumeButtonClick()
    {
        isPaused = false;
        gameObject.SetActive(isPaused);
        GameManager.instance.PlayPause();
    }
}
