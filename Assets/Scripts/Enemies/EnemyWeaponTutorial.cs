using UnityEngine;
using UnityEngine.Events;

public class EnemyWeaponTutorial : MonoBehaviour
{
    public GameObject arrow;
    public Transform arrowOrigin;
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