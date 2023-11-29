using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWeaponTutorial : MonoBehaviour
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