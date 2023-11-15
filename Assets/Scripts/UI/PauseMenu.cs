using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(GameManager.instance.isPaused);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleOnState()
    {

        gameObject.SetActive(GameManager.instance.isPaused);
    }

    public void onClickMainMenu()
    {
        GameManager.instance.GameOver();
    }
}
