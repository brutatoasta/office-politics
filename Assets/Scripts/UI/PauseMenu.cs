using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isActive;
    void Start()
    {
        isActive = false;
        gameObject.SetActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleOnState()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);
    }

    public void onClickMainMenu()
    {
        GameManager.instance.GameOver();
    }
}
