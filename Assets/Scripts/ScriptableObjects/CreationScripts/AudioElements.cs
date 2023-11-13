using UnityEngine;

[CreateAssetMenu(fileName = "AudioElements", menuName = "ScriptableObjects/AudioElements", order = 6)]
public class AudioElements : ScriptableObject
{
    /* Description:
    This scriptable object serves as a container to hold all the audio elements used in the game. Each AudioElement's
    AudioType and AudioClip need to be assigned via Unity Inspector. Any script that needs an AudioClip to be played
    would reference the respective AudioElement in this scriptable object when raising an AudioElementGameEvent
    through the singleton GameManager's "PlayAudioElement(AudioElement audioElement)" function.

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