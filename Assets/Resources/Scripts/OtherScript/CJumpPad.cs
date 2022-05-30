using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJumpPad : CStaticObject
{

    [SerializeField] private float m_Force;
    private CPlayer m_Player;
    private Rigidbody rb;

    [SerializeField] private Animator Animator;

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.transform.CompareTag("Player"))
        {   
            if (rb == null)
                rb = collision.transform.GetComponent<Rigidbody>();

            if (m_Player == null)
                m_Player = collision.transform.GetComponent<CPlayer>();


            if (m_Player.CompareController("Jump"))
            {
                m_Player.DestroyController("Jump");
            }

            rb.velocity = Vector3.zero;
            rb.velocity = Vector3.up * m_Force;

            //애니메 트리거
            Animator.SetTrigger("PlayerTrigger");


        }
    }
    

}
