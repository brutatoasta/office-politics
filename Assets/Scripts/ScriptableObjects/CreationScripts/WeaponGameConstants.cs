using UnityEngine;

[CreateAssetMenu(fileName = "WeaponGameConstants", menuName = "ScriptableObjects/WeaponGameConstants")]
public class WeaponGameConstants : ScriptableObject
{
    // Arrow speeds
    public float stressArrowSpeed;
    public float jobArrowSpeed;
    public float fanArrowSpeed;
    public float jobArrowMaxForce;

    public int fanArrowDamage;
    public int stressArrowDamage;
    public int jobArrowDamage;
}