using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Player Constants
    public PlayerConstants playerConstants;
    public WeaponGameConstants arrowConstants;
    public AudioElements audioElements;


    // movement

    Vector2 movementInput;
    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new();
    public float collisionOffset = 0.05f;

    // components
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
        GameManager.instance.cycleInventory.AddListener(CycleConsumable);
        GameManager.instance.TimerStop.AddListener(OnOvertime);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Debug.Log(movementInput);
            // If movement input is not 0, move player
            if (movementInput != Vector2.zero)
            {
                // if less than max, add a force to accelerate it in the next fixed update frame
                // force is in the direction of movement input (normalized) multiplied by movespeed
                if (rb.velocity.magnitude < playerConstants.maxMoveSpeed)
                {
                    bool success = TryMove(movementInput);

                    if (!success)
                    {
                        success = TryMove(new Vector2(movementInput.x, 0));
                    }

                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                    
                    if( !success) Debug.Log("really can't move");
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

    private void DoMove(Rigidbody2D rb) => rb.AddForce(movementInput * playerConstants.moveSpeed);
    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            // we square time because movement updates are 
            float distanceCast = playerConstants.moveSpeed * Time.fixedDeltaTime * Time.fixedDeltaTime;
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                distanceCast + collisionOffset); // The distance to cast equal to the movement plus an offset

            if (count == 0)
            {
                DoMove(rb);
                return true;
            }
            else
            {
                return false;
            }
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
        GameManager.instance.PlayAudioElement(audioElements.playerDash);

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
        GameManager.instance.PlayAudioElement(audioElements.playerParry);

        Collider2D[] parriedArrows = Physics2D.OverlapCircleAll(transform.position, playerConstants.parryRange);

        foreach (Collider2D arrow in parriedArrows)
        {
            if (arrow.gameObject.CompareTag("Arrow"))
            {
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

            else if (arrow.gameObject.CompareTag("Enemy"))
            {
                Vector2 reflectionNormal = (arrow.attachedRigidbody.position - rb.position).normalized;
                arrow.attachedRigidbody.AddForce(reflectionNormal * 10, ForceMode2D.Impulse);
            }
        }
        yield return new WaitForSecondsRealtime(playerConstants.parryCooldown);
        canParry = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerConstants.parryRange);
    }


    void UseConsumable()
    {
        // audioSource.PlayOneShot(playerConstants.useConsumeableClip);
        GameManager.instance.PlayAudioElement(audioElements.useConsumable);
    }

    void CycleConsumable(int _)
    {
        // audioSource.PlayOneShot(playerConstants.cycleConsumeableClip);
        GameManager.instance.PlayAudioElement(audioElements.cycleConsumable);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Arrow") && !invincible)
        {
            if (col.gameObject.name.Contains("JobArrow"))
            {
                GameManager.instance.IncreaseJob();
            }
            if (col.gameObject.name.Contains("StressArrow"))
            {
                GameManager.instance.levelVariables.stressPoints += arrowConstants.stressArrowDamage;
                GameManager.instance.IncreaseStress();
            }
            GameManager.instance.PlayAudioElement(audioElements.playerGetHitIntensity1);
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
