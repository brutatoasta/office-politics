using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    // Player Constants
    public PlayerConstants playerConstants;
    public WeaponGameConstants arrowConstants;
    public ParticleSystem trailParticles;
    public ParticleSystem coffeeParticles;
    public ParticleSystem kitkatParticles;
    public ParticleSystem adderallParticles;

    Vector2 movementInput;
    SpriteRenderer playerSprite;
    Rigidbody2D rb;
    Animator animator;
    Animator handAnimator;

    Vector3 teleportToOffice = new(-17, -3, 0);

    Vector3 teleportToBossRoom = new(-43, -3, 0);

    public TrailRenderer trail;
    [SerializeField]
    public bool canMove = true;
    bool canDash = true;
    bool canParry = true;

    bool interactLock = false;

    bool trailActive = false;
    float coffeeActiveTime = 0f;
    float adderallActiveTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        handAnimator = transform.GetChild(0).GetComponent<Animator>();

        GameManager.instance.useConsumable.AddListener(UseConsumable);
        GameManager.instance.updateInventory.AddListener(CycleConsumable);
        GameManager.instance.consumableEfffect.AddListener(ApplyConsumableEffect);
        GameManager.instance.playerFreeze.AddListener(AcquireInteractLock);
        GameManager.instance.playerUnFreeze.AddListener(ReleaseInteractLock);
        GameManager.instance.TimerStop.AddListener(OnOvertime);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // If movement input is not 0, move player
            if (movementInput != Vector2.zero)
            {
                if (rb.velocity.magnitude < playerConstants.maxMoveSpeed)
                {
                    rb.AddForce(movementInput * playerConstants.moveSpeed);
                }

                // natthan - play walking sound if player is moving
                if (rb.velocity != Vector2.zero)
                {
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerWalk);
                }
                // natthan - stop the walking sound if the player is not moving (e.g. blocked by obstacle)
                else if (rb.velocity == Vector2.zero)
                {
                    GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerStop);
                }
            }
            else
            {
                rb.velocity = Vector2.zero;

                // natthan - stop awalking sound
                GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerStop);
            }
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

        // Particles
        if (rb.velocity.magnitude > 0.3 && !trailActive)
        {
            trailParticles.Play();
            trailActive = true;
        }
        if (rb.velocity.magnitude <= 0.3 && trailActive)
        {
            trailParticles.Stop();
            trailActive = false;
        }

        // Coffee Particles
        if (coffeeActiveTime >= 5f) coffeeParticles.Play();
        if (coffeeActiveTime <= 0f) coffeeParticles.Stop();
        if (coffeeActiveTime > 0) coffeeActiveTime -= Time.deltaTime;

        // Adderall Particles
        if (adderallActiveTime >= 10f) adderallParticles.Play();
        if (adderallActiveTime <= 0f) adderallParticles.Stop();
        if (adderallActiveTime > 0) adderallActiveTime -= Time.deltaTime;
    }

    public void MoveCheck(Vector2 movement)
    {
        movementInput = movement.normalized;
    }

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

        List<int> oldArrows = new();

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
                kitkatParticles.Play();
                break;

            case ConsumableType.Coffee:
                coffeeActiveTime = 5f;
                break;

            case ConsumableType.Adderall:
                adderallActiveTime = 10f;
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
            GameManager.instance.endingVariables.Ninja = false;
            if (col.gameObject.name.Contains("JobArrowTutorial"))
            {
                GameManager.instance.IncreaseCoffeeJob();
            }
            else if (col.gameObject.name.Contains("JobArrow"))
            {
                GameManager.instance.IncreaseJob();
            }
            else if (col.gameObject.name.Contains("FanArrow"))
            {
                GameManager.instance.levelVariables.stressPoints += arrowConstants.fanArrowDamage;
                GameManager.instance.IncreaseStress();
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
