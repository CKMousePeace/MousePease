using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCollider : MonoBehaviour
{

    public float g_Radius = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, g_Radius);
    }


}
