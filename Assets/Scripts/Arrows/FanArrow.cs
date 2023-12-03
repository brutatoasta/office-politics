using Pathfinding;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FanArrow : BaseArrow
{
    private float speed;

    public override void OnParry() { }
    private TrailRenderer trailRenderer;
    private void Awake()
    {
        speed = weaponGameConstants.fanArrowSpeed;
        throwArrowAudioElement = GameManager.instance.audioElements.throwStressArrow;
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }
    public void Shoot2(Transform bossCoords, float theta)
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        float theta_rad = Mathf.Deg2Rad * theta;

        Vector3 direction = player.transform.position - bossCoords.position;      
        Vector2 direction_norm = (Vector2)direction.normalized;

        float bx = direction_norm.x * Mathf.Cos(theta_rad) - direction_norm.y * Mathf.Sin(theta_rad);
        float by = direction_norm.x * Mathf.Sin(theta_rad) + direction_norm.y * Mathf.Cos(theta_rad);

        Vector2 new_direction_norm = new Vector2(bx,by);

        rb.velocity = new_direction_norm * speed;
        //rb.velocity = new Vector2(10,10);

        float rot = Mathf.Atan2(-new_direction_norm.y, -new_direction_norm.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 45);

        GameManager.instance.PlayAudioElement(throwArrowAudioElement);
    }
}