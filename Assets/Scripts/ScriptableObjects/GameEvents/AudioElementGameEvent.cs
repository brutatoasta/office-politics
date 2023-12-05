using UnityEngine;

[CreateAssetMenu(fileName = "AudioElementGameEvent", menuName = "ScriptableObjects/AudioElementGameEvent")]
public class AudioElementGameEvent : GameEvent<AudioElement>
{
    /* Description:
    This class is used to create the "onPlayAudioElement" scriptable object game event. It facilitates the passing of
    an AudioElement from a script to the AudioManager.
    
    Any script that needs an AudioClip to be played would reference the respective AudioElement from the
    "AudioElements" scriptable object when raising the "onPlayAudioElement" AudioElementGameEvent through the
    GameManager's "PlayAudioElement(AudioElement audioElement)" function.

    When "onPlayAudioElement" is raised, its AudioElement is received by the AudioManager's
    AudioElementGameEventListener, which in turn calls the AudioManager's "PlayAudioElement(AudioElement
    audioElement)" method
    */
}