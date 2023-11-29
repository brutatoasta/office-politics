using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Player Constants
    public PlayerConstants playerConstants;
    public WeaponGameConstants arrowConstants;

    Vector2 movementInput;
    SpriteRenderer playerSprite;
    SpriteRenderer heldSprite;
    Rigidbody2D rb;
    Animator animator;
    Animator handAnimator;
    // AudioSource audioSource;

    Vector3 teleportToOffice = new Vector3(-17, -3, 0);

    Vector3 teleportToBossRoom = new Vector3(-43, -3, 0);

    public TrailRenderer trail;

    bool canMove = true;
    bool canDash = true;
    bool canParry = true;
    bool invincible = false;

    public bool touching = false;
    new Collider2D collider;
    bool interactLock = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
        heldSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        handAnimator = transform.GetChild(0).GetComponent<Animator>();

        GameManager.instance.useConsumable.AddListener(UseConsumable);
        GameManager.instance.updateInventory.AddListener(CycleConsumable);
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
            }
            else
            {
                rb.velocity = Vector2.zero;
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

    public void MoveCheck(Vector2 movement)
    {
        movementInput = movement;
    }

    public void TriggerInteract()
    {
        if (!interactLock)
        {
            GameManager.instance.interact.Invoke();
        }


        // // the object player is interacting with
        // if (collider != null)
        // {
        //     BaseInteractable inter = collider.gameObject.GetComponent<BaseInteractable>();
        //     // check if player is touching object and not currently animating
        //     if (touching && !interactLock)
        //     {
        //         if (collider.gameObject.layer == 8)
        //         {
        //             // get script component and cast according to type field
        //             // check if held object is valid
        //             if (inter.CastAndInteract(heldSprite))
        //             {

        //             }
        //         }

        //     }
        // }
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

        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.playerParry);

        List<int> oldArrows = new List<int>();

        float timePassed = 0f;
        while (timePassed < 0.6f)
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


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerConstants.parryRange);
    }


    void UseConsumable()
    {
        // audioSource.PlayOneShot(playerConstants.useConsumeableClip);
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.useConsumable);
    }

    void CycleConsumable()
    {
        // audioSource.PlayOneShot(playerConstants.cycleConsumeableClip);
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.cycleConsumable);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Arrow") && !invincible)
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
        invincible = true;
        playerConstants.moveSpeed -= 10;
        for (int i = 0; i < 6; i++)
        {
            playerSprite.material = (i % 2 == 0) ? playerConstants.hurtMaterial : playerConstants.defaultMaterial;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        playerConstants.moveSpeed += 10;
        invincible = false;
    }

    public void OnOvertime()
    {
        InvokeRepeating("TickOvertime", 0, 1.0f);
    }

    public void TickOvertime()
    {
        GameManager.instance.levelVariables.stressPoints += playerConstants.overtimeTick;
        GameManager.instance.IncreaseStress();
    }


    // Interact with objects
    // nervous system, tells you if you're touching
    void OnTriggerStay2D(Collider2D col)
    {
        touching = true;
        collider = col;

        // dont allow holding more stuff if already holding something
    }
    void OnTriggerExit2D(Collider2D col)
    {
        touching = false;
        collider = null;
    }

}
