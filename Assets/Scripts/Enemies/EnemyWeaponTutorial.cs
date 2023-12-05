using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWeaponTutorial : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowOrigin;

    void Start()
    {

    }


    void Update()
    {

    }
    public UnityEvent<Transform> Spawn;
    public void StartArrowSequence()
    {
        if (gameObject.GetComponent<EnemyWeaponTutorial>().enabled)
        {
            Instantiate(arrow, this.transform);
            Spawn.Invoke(transform);
        }
    }
}