using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

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
