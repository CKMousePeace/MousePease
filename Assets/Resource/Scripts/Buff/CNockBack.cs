using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CNockBack : CBuffBase
{
    private Vector3 m_Dir;
    private float m_Force;
    private float m_Damage;

    [SerializeField] private CColliderChecker m_ColliderChecker;


    private void OnEnable()
    {
        g_DynamicObject.g_Animator.SetTrigger("Hit");
        g_DynamicObject.g_Animator.SetBool("isGround", false);
    }
    private void OnDisable()
    {
        g_DynamicObject.g_Animator.SetBool("isGround", true);
    }
    //Buff √ ±‚»≠
    protected override void OnBuffInit(BuffInfo buff)
    {
        try
        {
            m_Dir = (Vector3.up + (transform.position - buff.g_Value_Vector3[0]) * 0.3f).normalized;
            m_Force = buff.g_Value_Float[0];
            m_Damage = buff.g_Value_Float[1];

            g_DynamicObject.g_Rigid.AddForce(m_Dir * m_Force * 3.0f, ForceMode.Impulse);
            g_DynamicObject.g_fHP -= m_Damage;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public override void init(CDynamicObject dynamicObject)
    {
        base.init(dynamicObject);
        m_ColliderChecker.m_ColliderEnter += CollierEnter;        
    }


    private void CollierEnter(Collision collder)
    {
        var extents = new Vector3(0.0f, m_ColliderChecker.g_Collider.bounds.extents.y, 0.0f);
        if (g_DynamicObject.g_Rigid.velocity.y <= 0)
        {
            if (Physics.Raycast(m_ColliderChecker.transform.position - (extents * 0.9f), -Vector3.up, 0.3f))
            {
                gameObject.SetActive(false);
            }
        }
    }


}
