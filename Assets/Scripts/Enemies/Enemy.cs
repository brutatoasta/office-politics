using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    public EnemyGameConstants constants;

    bool isChasing = false;

    private void Awake()
    {
        gameObject.GetComponent<EnemyWeapon>().enabled = isChasing;
        gameObject.GetComponent<AIPath>().enabled = isChasing;
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
    }

    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        gameObject.GetComponent<EnemyWeapon>().enabled = isChasing;
        gameObject.GetComponent<AIPath>().enabled = isChasing;
    }
}
