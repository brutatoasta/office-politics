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
    public AudioElement lobbyBGM;
    public AudioElement gameplayBGMIntensity1;
    public AudioElement gameplayBGMIntensity2;
    public AudioElement gameplayBGMIntensity3;
    public AudioElement bedroomBGM;
    public AudioElement badEndingBGM;
    public AudioElement goodEndingBGM;

    // UI SFX
    public AudioElement menuClick;
    public AudioElement menuBack;
    public AudioElement gamePause;
    public AudioElement gameResume;
    public AudioElement cycleConsumable;
    public AudioElement showTaskDetails;
    public AudioElement hideTaskDetails;
    public AudioElement purchaseItem;
    public AudioElement warnTime;
    public AudioElement increaseTaskQuota;
    public AudioElement reduceTaskQuota;
    public AudioElement taskComplete;
    public AudioElement levelComplete;
    public AudioElement cannotPurchaseItem;
    public AudioElement overtimeStart;

    // Player SFX
    public AudioElement playerWalk;
    public AudioElement playerStop;
    public AudioElement playerDash;
    public AudioElement playerParry;
    public AudioElement playerParrySuccess;
    public AudioElement useConsumable;
    public AudioElement playerGetHitIntensity1;
    public AudioElement playerGetHitIntensity2;
    public AudioElement playerGetHitIntensity3;

    // Enemy SFX
    public AudioElement throwArrow;
    public AudioElement throwJobArrow;
    public AudioElement prepareStressArrow;
    public AudioElement throwStressArrow;
    public AudioElement prepareFanArrow;
    public AudioElement throwFanArrow;
    public AudioElement enemyGetStunned;

    // NPC SFX

    // Interaction SFX
    public AudioElement closeDoor;
    public AudioElement denyExit;
    public AudioElement pourDrink;
    public AudioElement serveRefreshment;
    public AudioElement refillCoffeePot;
    public AudioElement spillDrink;
    public AudioElement pickUpDocument;
    public AudioElement shredDocument;
    public AudioElement laminateDocument;
    public AudioElement jamMachine;
    public AudioElement photocopyDocument;
    public AudioElement throwTrash;
    public AudioElement interactionInvalid;
}