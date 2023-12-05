using UnityEngine;

public class AddPP : MonoBehaviour
{
    // for addition of PP animation, animator to destroy itself
    public void DeleteSelf()
    {
        Destroy(transform.parent.gameObject);
    }
}
