using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowOrigin;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("StartArrowSequenceWithPause", 0f,8f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public UnityEvent<Transform> Spawn;
    private void StartArrowSequence()
    {
        if (gameObject.GetComponent<EnemyWeapon>().enabled)
        {
            Instantiate(arrow, this.transform);
            Spawn.Invoke(transform);
        }
    }
    IEnumerator StartArrowSequenceWithPauseCoroutine()
    {
        yield return new WaitForSeconds(1f);
        StartArrowSequence();
    }
    private void StartArrowSequenceWithPause()
    {
        StartCoroutine(StartArrowSequenceWithPauseCoroutine());
    }
    public void PauseArrowShooting(bool isChasing)
    {
        //gameObject.GetComponent<EnemyWeapon>().enabled = !gameObject.GetComponent<EnemyWeapon>().enabled;
        gameObject.GetComponent<EnemyWeapon>().enabled = isChasing;
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