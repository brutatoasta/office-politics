using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    PlayerConstants playerConstants;

    void Start()
    {
        // GameManager.instance.increasePerformancePoint.AddListener(IncreasePP);
        playerConstants = GameManager.instance.playerConstants;
    }

    // void IncreasePP(int amount, Transform tf)
    // {
    //     playerConstants.performancePoints += amount;

    //     // TODO: trigger of animation
    //     // tmp.text = playerConstants.performancePoints // set current performance points
    //     // tmp.transform = tf
    //     // animator.SetTrigger("Increased"); // move up, fade away
    // }
}