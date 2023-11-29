using UnityEngine;

[CreateAssetMenu(fileName = "AudioElements", menuName = "ScriptableObjects/AudioElements")]
public class AudioElements : ScriptableObject
{
    /* Description:
    This class is used to create the "AudioElements" scriptable object.

    "AudioElements" serves as a container to hold all every AudioElement used in the game. Each AudioElement's
    AudioType and AudioClip need to be assigned via Unity Inspector.
    
    Any script that needs an AudioClip to be played would reference the respective AudioElement from "AudioElements"
    when raising the "onPlayAudioElement" AudioElementGameEvent through the GameManager's
    "PlayAudioElement(AudioElement audioElement)" function.

    Static.
    */

    /* Note:
    For this class, abbreviations like SFX are kept fully capitalised for better readability in Unity's Inspector.
     - e.g., using "playerSFX" (instead of "playerSfx") in the script results in "Player SFX" (instead of
     "Player Sfx") in the Inspector 
    */

    // BGM
    public AudioElement startMenuBGM;
    public AudioElement pantryBGM;
    public AudioElement gameplayBgmIntensity1;
    public AudioElement gameplayBgmIntensity2;
    public AudioElement gameplayBgmIntensity3;
    public AudioElement levelComplete;

    // UI SFX
    public AudioElement cycleConsumable;
    public AudioElement menuClick;
    public AudioElement menuBack;

    // Player SFX
    public AudioElement playerDash;
    public AudioElement playerParry;
    public AudioElement useConsumable;
    public AudioElement playerGetHitIntensity1;
    public AudioElement playerGetHitIntensity2;
    public AudioElement playerGetHitIntensity3;

    // Enemy SFX
    public AudioElement throwStressArrow;

    // NPC SFX

    // Interaction SFX
    public AudioElement pourCoffee;
    public AudioElement serveCoffee;
    public AudioElement spillCoffee;
    public AudioElement pickUpDocument;
    public AudioElement shredDocument;
    public AudioElement laminateDocument;
    public AudioElement jamMachine;
}