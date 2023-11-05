using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConstants", menuName = "ScriptableObjects/PlayerConstants", order = 1)]
public class PlayerConstants : ScriptableObject
{
    public int moveSpeed;
    public int maxMoveSpeed;
    public int stressPoint;
    public int maxStressPoint;
    public int sanityPoint;
    public int performancePoint;


}