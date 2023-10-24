using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowOrigin;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(arrow, arrowOrigin.position, Quaternion.identity);
    }
}
