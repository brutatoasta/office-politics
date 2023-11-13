public enum AudioType
{
    /* Description:
    This enum is used in the AudioElement struct, to allow each AudioElement in the AudioElements scriptable object
    to have their AudioType easily configured via Unity Inspector.

    An AudioElement's AudioType is used by the AudioManager to determine which AudioSource should be used to play the
    AudioElement's AudioClip. See "AudioManager.cs" for more information.
    */

    /* Note:
    For this enum, abbreviations like SFX are kept fully capitalised for better readability in Unity's Inspector.
      - e.g., using "playerSFX" (instead of "playerSfx") in the script results in "Player SFX" (instead of
      "Player Sfx") in the Inspector 
    */

    BGM,
    gameplayBGMIntensity1,
    gameplayBGMIntensity2,
    gameplayBGMIntensity3,
    userInterfaceSFX,
    playerSFX,
    enemySFX,
    nonPlayableCharaterSFX,
    interactionSFX
}