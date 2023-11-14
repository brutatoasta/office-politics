using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meetings : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject documentSprite;
    void Start()
    {
        GameManager.instance.showMeetingDocs.AddListener(ShowDocument);
    }
    void ShowDocument()
    {
        documentSprite.SetActive(true);
    }
}
