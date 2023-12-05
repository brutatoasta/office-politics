using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConstants", menuName = "ScriptableObjects/PlayerConstants")]
public class PlayerConstants : ScriptableObject
{
    public int moveSpeed;
    public int maxMoveSpeed;
    public float dashPower;
    public float dashTime;
    public float dashCooldown;
    public float parryTime;
    public float parryRange;
    public float parryStartupTime;
    public float parryCooldown;
    public Material hurtMaterial;
    public Material invincibleMaterial;
    public Material defaultMaterial;

    // starting values
    public int overtimeTick;

    public Sprite parryIcon;
    public Sprite dashIcon;
    public void Init()
    {
        parryCooldown = 4f;
        dashCooldown = 3f;
        moveSpeed = 50;
        maxMoveSpeed = 60;
    }

}