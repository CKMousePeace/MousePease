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
        m_ColliderChecker.m_ColliderEnter += CollierEnter;
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
        
    }


    //충돌이 일어 날 때 함수 입니다.
    private void CollierEnter(Collision collder)
    {        
        var extents = new Vector3(0.0f, m_ColliderChecker.g_Collider.bounds.extents.y, 0.0f);
        if (g_DynamicObject.g_Rigid.velocity.y <= 0)
        {
            // 레이케스트를 사용해서 아래에 물체가 있으면 버프가 종료 됩니다.
            if (Physics.Raycast(m_ColliderChecker.transform.position - (extents * 0.9f), -Vector3.up, 0.3f))
            {
                gameObject.SetActive(false);
            }
        }
    }


}
