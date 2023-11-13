using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioElementGameEventListener : GameEventListener<AudioElement>
{
    /* Description
    This AudioElementGameEventListener is attached to the AudioManager GameObject. The AudioManager subscribes to the
    "onPlayAudioElement" AudioElementGameEvent.

    When "onPlayAudioElement" is raised by a script through the GameManager, its AudioElement is passed to the
    AudioManager's AudioElementGameEventListener, which in turn calls the AudioManager's
    "PlayAudioElement(AudioElement audioElement)" method.
    */
}