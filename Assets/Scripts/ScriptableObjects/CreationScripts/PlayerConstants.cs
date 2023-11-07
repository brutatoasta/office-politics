using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConstants", menuName = "ScriptableObjects/PlayerConstants", order = 1)]
public class PlayerConstants : ScriptableObject
{
    public int moveSpeed;
    public int maxMoveSpeed;
    public float dashPower;
    public float dashTime;
    public float dashCooldown;
    public float parryRange;
    public float parryStartupTime;
    public float parryCooldown;


    public int stressPoint;
    public int maxStressPoint;
    public int sanityPoint;
    public int performancePoint;


}