using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGameConstants", menuName = "ScriptableObjects/GameConstants", order = 2)]
public class EnemyGameConstants : ScriptableObject
{
    // Boss's movement
    public float speed;
    public int arrowTimeInterval;
}