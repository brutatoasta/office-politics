using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGameConstants", menuName = "ScriptableObjects/EnemyGameConstants")]
public class EnemyGameConstants : ScriptableObject
{
    // Boss's movement
    public float speed;
    public int arrowTimeInterval;
}