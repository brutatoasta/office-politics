using UnityEngine;
public abstract class BaseArrow : MonoBehaviour, IArrow
{
    public ArrowTypes type;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnArrow()
    {

    }
    public abstract void MoveToPlayer();
}
