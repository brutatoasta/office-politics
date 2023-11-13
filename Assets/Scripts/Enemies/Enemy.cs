using Pathfinding;
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
        gameObject.GetComponent<EnemyWeapon>().enabled = isChasing;
        gameObject.GetComponent<AIPath>().enabled = isChasing;
        enemyBody = gameObject.GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        ComputeVelocity();
    }

    public float Health {
        set {
            health = value;

            if(health <= 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }

    public float health = 1;

    private float speed;
    private void Start() {
        animator = GetComponent<Animator>();
        speed = constants.speed;
    }

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy() {
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
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void MoveBoss()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

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
                }
                else
                {
                    // change direction
                    moveRight *= -1;
                    ComputeVelocity();
                    MoveBoss();
                }
            }
            else //returning to original spot before patrolling again
            {
                if (Mathf.Abs(gameObject.transform.position.x - originalTransform.position.x) < 0.5 && 
                    Mathf.Abs(gameObject.transform.position.y - originalTransform.position.y) < 0.5)
                {
                    gameObject.GetComponent<AIPath>().enabled = false;
                    isReturning = false;
                } 
            }
        }
    }
}
