using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPP : MonoBehaviour
{
    public void DeleteSelf()
    {
        Destroy(transform.parent.gameObject);
    }
}
