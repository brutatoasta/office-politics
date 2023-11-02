using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer > 2)
        //{
        //    timer = 0;
        //    Shoot();
        //}
    }
    public void Shoot()
    {
        Instantiate(arrow, arrowOrigin.position, Quaternion.identity);
    }
}
public enum ArrowTypes
{
    JobArrow = 0,
    StressArrow = 1
}