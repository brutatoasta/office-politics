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
    public BoxCollider2D interactionCollider;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;


    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        interactionCollider.enabled = true;
    }
    public void ReleaseInteract()
    {
        interactionCollider.enabled = false;
    }

    public void StopInteract()
    {
        canMove = true;
        interactionCollider.enabled = false;
    }


    // Interact with objects
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            canMove = false;
            animator.SetTrigger("interact");

            rb.velocity = new Vector3();
            Interactable inter = col.gameObject.GetComponent<Interactable>();
            inter.OnInteract();
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            Interactable inter = col.gameObject.GetComponent<Interactable>();
            inter.OnInteract();
        }
    }

}
