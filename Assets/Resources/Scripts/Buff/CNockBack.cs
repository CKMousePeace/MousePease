using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CNockBack : CBuffBase
{
    private Vector3 m_Dir;
    private float m_Force;
    private float m_Damage;
    private float m_GroundCos;

    [SerializeField] private float m_MaxGroundAngle;
    [SerializeField] private CColliderChecker m_ColliderChecker;
    
    private void OnEnable()
    {
        g_DynamicObject.g_Animator.SetBool("isGround", false);
        m_ColliderChecker.m_ColliderEnter += CollierEnter;
        
        CPlayer play = (CPlayer)g_DynamicObject;
        play.SetColor();

    }
    private void OnDisable()
    {
        g_DynamicObject.g_Animator.SetBool("isGround", true);
        m_ColliderChecker.m_ColliderEnter -= CollierEnter;
        
    }
    //Buff 초기화
    protected override void OnBuffInit(BuffInfo buff)
    {
        //try로 걸어주는 이유는 받은 Buffinfo 가 없을 수 도 있기 때문에 예외 처리르 해주었습니다.
        try
        {
            m_Dir = (Vector3.up + (transform.position - buff.g_Value_Vector3[0])).normalized;
            m_Force = buff.g_Value_Float[0];
            m_Damage = buff.g_Value_Float[1];
            g_DynamicObject.g_Rigid.velocity = Vector3.zero;
            g_DynamicObject.g_Rigid.velocity += m_Dir * m_Force;
            g_DynamicObject.g_fHP -= m_Damage;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

        g_DynamicObject.g_Animator.SetTrigger("Hit");
    }

    public override void init(CDynamicObject dynamicObject)
    {
        base.init(dynamicObject);
        m_GroundCos = Mathf.Cos(Mathf.Deg2Rad * m_MaxGroundAngle);
    }

    //충돌이 일어 날 때 함수 입니다.
    private void CollierEnter(Collision collder)
    {   
        for (int i = 0; i < collder.contactCount; i++)
        {
            // 레이케스트를 사용해서 아래에 물체가 있으면 버프가 종료 됩니다.
            if (collder.GetContact(i).normal.y > m_GroundCos)
            {
                gameObject.SetActive(false);
            }
        }
    }


}
