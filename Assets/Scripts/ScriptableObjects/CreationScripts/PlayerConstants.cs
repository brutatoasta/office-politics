using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConstants", menuName = "ScriptableObjects/PlayerConstants")]
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

    // starting values
    public int stressPoint;
    public int maxStressPoint;
    public int sanityPoint;
    public int performancePoints;



}