using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// no arguments
[CreateAssetMenu(fileName = "AudioElementGameEvent", menuName = "ScriptableObjects/AudioElementGameEvent", order = 3)]
public class AudioElementGameEvent : GameEvent<AudioElement>
{
    /* Description:
    This class is used to create the "onPlayAudioElement" scriptable object game event.

    "onPlayAudioElement" facilitates the passing of an AudioElement from a script to the AudioManager.

    "onPlayAudioElement" will be raised by the GameManager's "PlayAudioElement(AudioElement audioElement)" method,
    which is called by any other script that needs the AudioManager to play a specific audio element.

    When "onPlayAudioElement" is raised, its AudioElement is passed to the AudioManager's
    AudioElementGameEventListener, which in turn calls the AudioManager's "PlayAudioElement(AudioElement
    audioElement)" method
    */
}