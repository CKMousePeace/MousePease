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
    //Buff �ʱ�ȭ
    protected override void OnBuffInit(BuffInfo buff)
    {
        //try�� �ɾ��ִ� ������ ���� Buffinfo �� ���� �� �� �ֱ� ������ ���� ó���� ���־����ϴ�.
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

    //�浹�� �Ͼ� �� �� �Լ� �Դϴ�.
    private void CollierEnter(Collision collder)
    {   
        for (int i = 0; i < collder.contactCount; i++)
        {
            // �����ɽ�Ʈ�� ����ؼ� �Ʒ��� ��ü�� ������ ������ ���� �˴ϴ�.
            if (collder.GetContact(i).normal.y > m_GroundCos)
            {
                gameObject.SetActive(false);
            }
        }
    }


}
