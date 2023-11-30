using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Constants
    public PlayerConstants playerConstants;
    public WeaponGameConstants arrowConstants;

    // movement
    public Vector2 movementInput;
    public ContactFilter2D movementFilter;
    public List<RaycastHit2D> castCollisions = new();
    public float collisionOffset;
    public float distanceCast;
    // components
    SpriteRenderer playerSprite;

    Rigidbody2D rb;
    public float currentSpeed;
    Animator animator;
    Animator handAnimator;

    Vector3 teleportToOffice = new(-17, -3, 0);

    Vector3 teleportToBossRoom = new(-43, -3, 0);

    public TrailRenderer trail;

    [SerializeField]
    bool canMove = true;
    bool canDash = true;
    bool canParry = true;


    public bool touching = false;

    [SerializeField]
    bool interactLock = false;
    // Start is called before the first frame update
    void Start()
    {
        distanceCast = playerConstants.moveSpeed * Time.fixedDeltaTime;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        handAnimator = transform.GetChild(0).GetComponent<Animator>();

        GameManager.instance.useConsumable.AddListener(UseConsumable);
        GameManager.instance.updateInventory.AddListener(CycleConsumable);
        GameManager.instance.consumableEfffect.AddListener(ApplyConsumableEffect);

        GameManager.instance.TimerStop.AddListener(OnOvertime);
    }

    private void FixedUpdate()
    {
        currentSpeed = rb.velocity.magnitude; // debug: report current speed in unity inspector
        if (canMove)
        {
            // Debug.LogError("" +movementInput);
            // If movement input is not 0, move player
            if (movementInput != Vector2.zero)
            {
                // if less than max, add a force to accelerate it in the next fixed update frame
                // force is in the direction of movement input (normalized) multiplied by movespeed
                if (rb.velocity.magnitude < playerConstants.maxMoveSpeed)
                {

                    DoMove(TryMoves());
                }

                // Natthan
                if (rb.velocity != Vector2.zero)
                {
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerWalk);
                }
                // stop the walking sound if the player is blocked by an obstacle
                else if (rb.velocity == Vector2.zero)
                {
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerStop);
                }
            }
            else
            {
                rb.velocity = Vector2.zero;

                // Stop walking sound - Natthan
                GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerStop);
            }
        }
        if (DialogueManager.isActive == true)
        {
            canMove = false;
            interactLock = true;
        }
        if (DialogueManager.isActive == false)
        {
            canMove = true;
            interactLock = false;
        }
    }

    private void DoMove(Vector2 direction) => rb.AddForce(direction.normalized * playerConstants.moveSpeed);
    private Vector2 TryMoves()
    {
        Vector2 x_only = new Vector2(movementInput.x, 0).normalized;
        Vector2 y_only = new Vector2(0, movementInput.y).normalized;
        Vector2[] vectors = new Vector2[] { movementInput, x_only, y_only };

        foreach (Vector2 currentVector in vectors)
        {
            if (TryMove(currentVector))
            {
                return currentVector;

            }
        }
        Debug.Log("really can't move");
        return Vector2.zero;
    }
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero) // still need to check here because sometimes may take zero component from movementInput with zero component from FixedUpdate
        {
            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                distanceCast + collisionOffset); // The distance to cast equal to the movement plus an offset
            Debug.LogError("count " + count);
            return count == 0;
        }
        else
        {
            // Can't move if there's no direction to move in
            return false;
        }

    }
    void Update()
    {
        animator.SetFloat("playerVelocityX", rb.velocity.x);
        animator.SetFloat("playerVelocityY", rb.velocity.y);
        animator.SetBool("playerVelXGreater", Math.Abs(rb.velocity.x) - Math.Abs(rb.velocity.y) > 0.3);
        animator.SetBool("holding", GameManager.instance.held != null);

        // control hand position
        handAnimator.SetFloat("playerVelocityX", rb.velocity.x);
        handAnimator.SetFloat("playerVelocityY", rb.velocity.y);
        handAnimator.SetBool("playerVelXGreater", Math.Abs(rb.velocity.x) - Math.Abs(rb.velocity.y) > 0.3);
    }

    public void MoveCheck(Vector2 movement) => movementInput = movement.normalized;
    public void TriggerInteract()
    {
        if (!interactLock)
        {
            GameManager.instance.interact.Invoke();
        }
    }
    public void AcquireInteractLock()
    {
        canMove = false;
        interactLock = true;
    }
    public void ReleaseInteractLock()
    {
        interactLock = false;
        canMove = true;
    }

    public void Evade()
    {
        if (GameManager.instance.levelVariables.evadeType == EvadeType.Dash)
        {
            if (canDash && canMove)
            {
                StartCoroutine(Dash());
            }
        }
        else
        {
            if (canParry)
            {
                StartCoroutine(Parry());
            }
        }
    }

    IEnumerator Dash()
    {
        canMove = false;
        canDash = false;
        rb.velocity = movementInput.normalized * playerConstants.dashPower;
        trail.emitting = true;
        // audioSource.PlayOneShot(playerConstants.dashAudio);
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerDash);

        GameManager.instance.playerEvade.Invoke(playerConstants.dashCooldown);

        yield return new WaitForSecondsRealtime(playerConstants.dashTime);
        trail.emitting = false;
        canMove = true;
        yield return new WaitForSecondsRealtime(playerConstants.dashCooldown);
        canDash = true;
    }

    IEnumerator Parry()
    {
        canParry = false;
        transform.GetChild(1).GetComponent<Animator>().SetTrigger("parry");
        yield return new WaitForSecondsRealtime(playerConstants.parryStartupTime);

        GameManager.instance.playerEvade.Invoke(playerConstants.parryCooldown);

        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerParry);

        List<int> oldArrows = new List<int>();

        float timePassed = 0f;
        while (timePassed < playerConstants.parryTime)
        {
            ParryObj(oldArrows);
            timePassed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(playerConstants.parryCooldown);
        canParry = true;
    }

    void ParryObj(List<int> oldArrows)
    {
        Collider2D[] parriedArrows = Physics2D.OverlapCircleAll(transform.position, playerConstants.parryRange);

        foreach (Collider2D arrow in parriedArrows)
        {
            if (arrow.gameObject.CompareTag("Arrow") && (!oldArrows.Contains(arrow.gameObject.GetInstanceID())))
            {
                GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerParrySuccess);

                oldArrows.Add(arrow.gameObject.GetInstanceID());

                Rigidbody2D arrowRb = arrow.attachedRigidbody;
                Vector2 reflectionNormal = (arrowRb.position - rb.position).normalized;

                if (Vector2.Dot(arrowRb.velocity, reflectionNormal) >= 0)
                {
                    Vector2 velSurfaceReflect = arrowRb.velocity;
                    Vector2 newVel = arrowRb.velocity - 2 * Vector2.Dot(arrowRb.velocity, reflectionNormal) * reflectionNormal;
                    arrow.attachedRigidbody.velocity = -newVel - 2 * Vector2.Dot(-newVel, velSurfaceReflect.normalized) * velSurfaceReflect.normalized;
                }
                else
                {
                    arrow.attachedRigidbody.velocity = arrowRb.velocity - 2 * Vector2.Dot(arrowRb.velocity, reflectionNormal) * reflectionNormal;
                }

                arrow.gameObject.GetComponent<BaseArrow>().OnParry();
            }
        }
    }


    void OnDrawGizmosSelected() => Gizmos.DrawWireSphere(transform.position, playerConstants.parryRange);
    void UseConsumable(int _) => GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.useConsumable);
    void CycleConsumable() => GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.cycleConsumable);

    void ApplyConsumableEffect(ConsumableType consumableType)
    {
        switch (consumableType)
        {
            case ConsumableType.KitKat:
                //TODO: Visual Effects if any
                break;

            case ConsumableType.Coffee:
                //TODO: Visual Effects if any
                break;

            case ConsumableType.Adderall:
                //TODO: Visual Effects if any
                break;

            case ConsumableType.Starman:
                StartCoroutine(InvincibilityVisuals());
                break;

        }
    }

    IEnumerator InvincibilityVisuals()
    {
        for (int i = 0; i < 20; i++)
        {
            playerSprite.material = (i % 2 == 0) ? playerConstants.invincibleMaterial : playerConstants.defaultMaterial;
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Arrow") && !GameManager.instance.invincible)
        {
            if (col.gameObject.name.Contains("JobArrowTutorial"))
            {
                GameManager.instance.IncreaseCoffeeJob();
            }
            else if (col.gameObject.name.Contains("JobArrow"))
            {
                GameManager.instance.IncreaseJob();
            }
            else if (col.gameObject.name.Contains("StressArrow"))
            {
                GameManager.instance.levelVariables.stressPoints += arrowConstants.stressArrowDamage;
                GameManager.instance.IncreaseStress();
            }
            GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerGetHitIntensity1);
            StartCoroutine(HurtPlayerShader());
        }
        //teleport from 
        if (col.CompareTag("OfficeDoor"))
        {
            transform.position = teleportToBossRoom;
        }
        if (col.CompareTag("BossDoor"))
        {
            transform.position = teleportToOffice;
        }
    }

    IEnumerator HurtPlayerShader()
    {
        GameManager.instance.invincible = true;
        playerConstants.moveSpeed -= 10;
        for (int i = 0; i < 6; i++)
        {
            playerSprite.material = (i % 2 == 0) ? playerConstants.hurtMaterial : playerConstants.defaultMaterial;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        playerConstants.moveSpeed += 10;
        GameManager.instance.invincible = false;
    }

    public void OnOvertime() => InvokeRepeating(nameof(TickOvertime), 0, 1.0f);


    public void TickOvertime()
    {
        GameManager.instance.levelVariables.stressPoints += playerConstants.overtimeTick;
        GameManager.instance.IncreaseStress();
    }
}
