using UnityEngine;

[CreateAssetMenu(fileName = "AudioConstants", menuName = "ScriptableObjects/AudioConstants", order = 6)]
public class AudioConstants : ScriptableObject
{
    public int moveSpeed;
    public int maxMoveSpeed;
    public float dashPower;
    public float dashTime;
    public float dashCooldown;
    public AudioClip dashAudio;
    public float parryRange;
    public float parryStartupTime;
    public float parryCooldown;
    public AudioClip parryAudio;


    public AudioClip useConsumeableClip;
    public AudioClip cycleConsumeableClip;


    public int stressPoint;
    public int maxStressPoint;
    public int sanityPoint;
    public int performancePoint;


}