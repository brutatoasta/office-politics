using UnityEngine;

[CreateAssetMenu(fileName = "AudioElements", menuName = "ScriptableObjects/AudioElements", order = 6)]
public class AudioElements : ScriptableObject
{
    /* Description:
    This class is used to create the "AudioElements" scriptable object.

    "AudioElements" serves as a container to hold all every AudioElement used in the game. Each AudioElement's
    AudioType and AudioClip need to be assigned via Unity Inspector.
    
    Any script that needs an AudioClip to be played would reference the respective AudioElement from "AudioElements"
    when raising the "onPlayAudioElement" AudioElementGameEvent through the GameManager's
    "PlayAudioElement(AudioElement audioElement)" function.
    */

    // BGM
    public AudioElement startMenuBGM;
    public AudioElement pantryBGM;
    public AudioElement gameplayBgmIntensity1;
    public AudioElement gameplayBgmIntensity2;
    public AudioElement gameplayBgmIntensity3;

    // UI SFX
    public AudioElement cycleConsumableAudioClip;

    // Player SFX
    public AudioElement dashAudioClip;
    public AudioElement parryAudioClip;
    public AudioElement useConsumableAudioClip;

    // Enemy SFX
    public AudioElement stressArrowAudioClip;

    // NPC SFX

    // Interaction SFX
    public AudioElement pourCoffeeAudioClip;
}