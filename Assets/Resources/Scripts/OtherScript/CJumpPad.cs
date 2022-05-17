using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJumpPad : CStaticObject
{

    [SerializeField] private float m_Force;
    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.transform.CompareTag("Player"))
        {            
            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.velocity = Vector3.up * m_Force;
        }
    }
    

}
