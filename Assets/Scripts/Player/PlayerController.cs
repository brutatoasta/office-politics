using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Player Constants
    public PlayerConstants playerConstants;


    Vector2 movementInput;

    SpriteRenderer spriteRenderer;
    SpriteRenderer heldSprite;
    Rigidbody2D rb;
    Animator animator;
    public bool touching = false;
    new Collider2D collider;


    bool canMove = true;
    bool interactLock = false;
    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        heldSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Debug.Log(heldSprite.sprite);
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
        if (touching && !interactLock)
        {
            animator.SetTrigger("interact");
            canMove = false;
            rb.velocity = new Vector3();
            if (collider.gameObject.layer == 8)
            {
                // check type of interactable through another script?
                // if collider.gameObject.
                BaseInteractable inter = collider.gameObject.GetComponent<BaseInteractable>();
                inter.OnInteract(heldSprite);

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


    // Interact with objects
    // nervous system, tells you if you're touching
    void OnTriggerStay2D(Collider2D col)
    {
        touching = true;
        collider = col;

        // dont allow holding more stuff if already holding something
    }
    // void OnTriggerEnter2D(Collider2D col)
    // {   

    //     if (col.gameObject.layer == 8 && touching)
    //     {
    //         canMove = false;
    //         rb.velocity = new Vector3();
    //         Interactable inter = col.gameObject.GetComponent<Interactable>();
    //         inter.OnInteract();
    //         // change held sprite if holdable

    //     }
    // }
    void OnTriggerExit2D(Collider2D col)
    {
        touching = false;
        collider = null;
        // if (col.gameObject.layer == 8)
        // {
        //     Interactable inter = col.gameObject.GetComponent<Interactable>();
        //     inter.OnInteract();
        //     // change held sprite if holdable
        //     // heldSprite.sprite = null;

        // }
    }

}
