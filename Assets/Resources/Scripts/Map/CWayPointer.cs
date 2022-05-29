using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWayPointer : MonoBehaviour
{
    public static Transform[] g_Points;

    private void Awake()
    {
        g_Points = new Transform[transform.childCount];
        for(int i =0; i< g_Points.Length; i++)
        {
            g_Points[i] = transform.GetChild(i);
        }
    }


}
