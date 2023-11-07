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
    public InventoryVariable inventory;



    Vector2 movementInput;

    SpriteRenderer heldSprite;
    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;
    public TrailRenderer trail;


    bool canMove = true;
    bool canDash = true;
    bool canParry = true;

    public bool touching = false;
    new Collider2D collider;
    bool interactLock = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        heldSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        Debug.Log(heldSprite.sprite);

        GameManager.instance.useConsumable.AddListener(UseConsumable);
        GameManager.instance.cycleInventory.AddListener(CycleConsumable);
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
    }

    void Update()
    {
        animator.SetFloat("playerVelocityX", rb.velocity.x);
        animator.SetFloat("playerVelocityY", rb.velocity.y);
        animator.SetBool("playerVelXGreater", Math.Abs(rb.velocity.x) - Math.Abs(rb.velocity.y) > 0.3);
    }

    public void MoveCheck(Vector2 movement)
    {
        movementInput = movement;
    }

    public void TriggerInteract()
    {
        // the object player is interacting with
        if (collider != null)
        {
            BaseInteractable inter = collider.gameObject.GetComponent<BaseInteractable>();
            // check if player is touching object and not currently animating
            if (touching && !interactLock)
            {

                // check if held object is valid
                animator.SetTrigger("interact");
                canMove = false;
                rb.velocity = new Vector3();
                if (collider.gameObject.layer == 8)
                {
                    // get script component and cast according to type field
                    switch (inter.type)
                    {
                        case InteractableType.Receivable:
                            Receivable re = (Receivable)inter;

                            re.OnInteract(heldSprite);


                            // check if player is holding object and allowed to deposit
                            break;
                        case InteractableType.Holdable:
                            Holdable ho = (Holdable)inter;
                            ho.OnInteract(heldSprite);
                            // check if player is holding object and allowed to pickup another
                            break;
                        default:
                            inter.OnInteract(heldSprite);
                            break;
                    }

                }
            }
        }





    }
    public void AcquireInteractLock()
    {
        interactLock = true;
    }
    public void ReleaseInteractLock()
    {
        interactLock = false;
        canMove = true;
    }

    public void Evade()
    {
        if (inventory.evadeType == EvadeType.Dash)
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
        audioSource.PlayOneShot(playerConstants.dashAudio);

        yield return new WaitForSecondsRealtime(playerConstants.dashTime);
        trail.emitting = false;
        canMove = true;
        yield return new WaitForSecondsRealtime(playerConstants.dashCooldown);
        canDash = true;
    }

    IEnumerator Parry()
    {
        yield return new WaitForSecondsRealtime(playerConstants.parryStartupTime);
        // audioSource.PlayOneShot(playerConstants.parryAudio);

        Collider2D[] parriedArrows = Physics2D.OverlapCircleAll(transform.position, playerConstants.parryRange);

        foreach (Collider2D arrow in parriedArrows)
        {
            if (arrow.gameObject.CompareTag("Arrow")) {
                Rigidbody2D arrowRb = arrow.attachedRigidbody;
                Vector2 reflectionNormal = (arrowRb.position - rb.position).normalized;
                arrow.attachedRigidbody.velocity = arrowRb.velocity - 2 * Vector2.Dot(arrowRb.velocity, reflectionNormal) * reflectionNormal;
            }
        }
        yield return null;
    }

    void UseConsumable()
    {
        audioSource.PlayOneShot(playerConstants.useConsumeableClip);
    }

    void CycleConsumable(int _)
    {
        audioSource.PlayOneShot(playerConstants.cycleConsumeableClip);
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
