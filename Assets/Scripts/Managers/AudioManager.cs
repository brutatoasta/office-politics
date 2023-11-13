using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* Description:
    This AudioManager is subscribed to the "onPlayAudioElement" AudioElementGameEvent.

    Any script that needs an AudioClip to be played would reference the respective AudioElement from the
    "AudioElements" scriptable object when raising the "onPlayAudioElement" AudioElementGameEvent through the
    GameManager's "PlayAudioElement(AudioElement audioElement)" function.

    The AudioManager chooses the appropriate AudioSource to play the raised AudioElement's AudioClip based on the
    AudioElement's AudioType. Each AudioSource is responsible for routing the AudioClip to the appropriate Audio Mixer
    group via its assigned outputAudioMixerGroup in Unity's Inspector.
    */

    /* Note:
    For this class, abbreviations like SFX are treated as normal words for better readability in the code editor.
     - e.g., "playerSfxAudioSource" instead of "playerSFXAudioSource"
    */

    public AudioElements audioElements;

    // audio sources
    private AudioSource bgmAudioSource;
    private AudioSource gameplayBgmIntensity1AudioSource;
    private AudioSource gameplayBgmIntensity2AudioSource;
    private AudioSource gameplayBgmIntensity3AudioSource;
    private AudioSource userInterfaceSfxAudioSource;
    private AudioSource playerSfxAudioSource;
    private AudioSource enemySfxAudioSource;
    private AudioSource nonPlayableCharacterSfxAudioSource;
    private AudioSource interactionSfxAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource = transform.Find("BGM").GetComponent<AudioSource>();
        gameplayBgmIntensity1AudioSource = transform.Find("BGM").transform.Find("Gameplay BGM Intensity 1").GetComponent<AudioSource>();
        gameplayBgmIntensity2AudioSource = transform.Find("BGM").transform.Find("Gameplay BGM Intensity 2").GetComponent<AudioSource>();
        gameplayBgmIntensity3AudioSource = transform.Find("BGM").transform.Find("Gameplay BGM Intensity 3").GetComponent<AudioSource>();
        userInterfaceSfxAudioSource = transform.Find("UI SFX").GetComponent<AudioSource>();
        playerSfxAudioSource = transform.Find("Player SFX").GetComponent<AudioSource>();
        enemySfxAudioSource = transform.Find("Enemy SFX").GetComponent<AudioSource>();
        nonPlayableCharacterSfxAudioSource = transform.Find("NPC SFX").GetComponent<AudioSource>();
        interactionSfxAudioSource = transform.Find("Interaction SFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudioElement(AudioElement audioElement)
    {
        switch (audioElement.audioType)
        {
            case AudioType.BGM:
                bgmAudioSource.clip = audioElement.audioClip;
                bgmAudioSource.Play();
                break;

            case AudioType.gameplayBGMIntensity1:
                gameplayBgmIntensity1AudioSource.clip = audioElement.audioClip;
                gameplayBgmIntensity1AudioSource.Play();
                break;

            case AudioType.gameplayBGMIntensity2:
                gameplayBgmIntensity2AudioSource.clip = audioElement.audioClip;
                gameplayBgmIntensity2AudioSource.Play();
                break;
            case AudioType.gameplayBGMIntensity3:
                gameplayBgmIntensity3AudioSource.clip = audioElement.audioClip;
                gameplayBgmIntensity3AudioSource.Play();
                break;
            case AudioType.userInterfaceSFX:
                userInterfaceSfxAudioSource.PlayOneShot(audioElement.audioClip);
                break;
            case AudioType.playerSFX:
                playerSfxAudioSource.PlayOneShot(audioElement.audioClip);
                break;
            case AudioType.enemySFX:
                enemySfxAudioSource.PlayOneShot(audioElement.audioClip);
                break;
            case AudioType.nonPlayableCharaterSFX:
                nonPlayableCharacterSfxAudioSource.PlayOneShot(audioElement.audioClip);
                break;
            case AudioType.interactionSFX:
                interactionSfxAudioSource.PlayOneShot(audioElement.audioClip);
                break;
            default:
                // Debug.Log("Error in AudioManager");
                break;
        }
    }
}
