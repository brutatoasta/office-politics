using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    public EnemyGameConstants constants;

    private Rigidbody2D enemyBody;
    private float originalX;
    private bool isChasing = false;
    private bool isReturning = false;
    private Vector2 velocity;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    public Transform originalTransform;
    private void Awake()
    {
        enemyBody = gameObject.GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        ComputeVelocity();
    }

    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    public float health = 1;

    private float positionX;
    private float positionY;
    private float speed;
    private void Start()
    {
        animator = GetComponent<Animator>();
        speed = constants.speed;
        positionX = transform.position.x;
        positionY = transform.position.y;

        gameObject.GetComponent<EnemyWeapon>().enabled = isChasing;
        gameObject.GetComponent<AIPath>().enabled = isChasing;
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void toggleState()
    {
        isChasing = !isChasing;
        if (isChasing)
        {
            gameObject.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag("Player").transform;
            gameObject.GetComponent<AIPath>().enabled = isChasing;
        }
        else
        {
            gameObject.GetComponent<AIDestinationSetter>().target = originalTransform;
            isReturning = true;
        }
        gameObject.GetComponent<EnemyWeapon>().enabled = isChasing;
    }
    private void UpdatePosition()
    {
        float velocityX = (transform.position.x - positionX) / 0.02f;
        float velocityY = (transform.position.y - positionY) / 0.02f;
        animator.SetFloat("bossVelocityX", velocityX);
        animator.SetFloat("bossVelocityY", velocityY);
        animator.SetBool("bossVelXGreater", Math.Abs(velocityX) - Math.Abs(velocityY) > 0.3);
        positionY = transform.position.y;
        positionX = transform.position.x;
    }
    private void FixedUpdate()
    {
        UpdatePosition();
    }

    public UnityEvent pauseEnemyWeapon;
    IEnumerator Stun()
    {
        GameManager.instance.PlayAudioElement(GameManager.instance.audioElements.enemyGetStunned);

        // stop the enemy first
        float existingSpeed = gameObject.GetComponent<AIPath>().maxSpeed;
        gameObject.GetComponent<AIPath>().maxSpeed = 0;
        animator.SetBool("isStun", true);

        // pause enemy weapon
        pauseEnemyWeapon.Invoke();
        yield return new WaitForSeconds(2f);

        //resume walking
        gameObject.GetComponent<AIPath>().maxSpeed = existingSpeed;
        animator.SetBool("isStun", false);

        // resume enemy weapon
        yield return new WaitForSeconds(6f); // because one enemy weapon cycle is 8s, cause enemy to miss next shooting chance
        if (isChasing)
        {
            pauseEnemyWeapon.Invoke();
        }
    }

    public void stunByArrow()
    {
        StartCoroutine(Stun());
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void MoveBoss()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    private bool eligibleToChangeDirection;
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (!isChasing)
        {
            if (!isReturning) //on patrol
            {
                if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
                {// move Boss
                    MoveBoss();
                    eligibleToChangeDirection = true;
                }
                else
                {
                    //Sometimes, update is function called before difference is below max offset, causing jittery boss
                    if (eligibleToChangeDirection)
                    {
                        // change direction
                        moveRight *= -1;
                        ComputeVelocity();
                        MoveBoss();
                        UpdatePosition();
                        eligibleToChangeDirection = false;
                    }
                }
            }
            else //returning to original spot before patrolling again
            {
                // margin of error because the object will not return to exactly the original transform
                if (Mathf.Abs(gameObject.transform.position.x - originalTransform.position.x) < 0.2 &&
                    Mathf.Abs(gameObject.transform.position.y - originalTransform.position.y) < 0.2)
                {
                    gameObject.GetComponent<AIPath>().enabled = false;
                    isReturning = false;
                }
            }
        }
    }

    public void ToggleAIPathParameter()
    {
        if (isChasing)
        {
            gameObject.GetComponent<AIPath>().slowdownDistance = 2;
            gameObject.GetComponent<AIPath>().endReachedDistance = 3;
        }
        else
        {
            gameObject.GetComponent<AIPath>().slowdownDistance = 0.6f;
            gameObject.GetComponent<AIPath>().endReachedDistance = 0.2f;
        }
    }
}
