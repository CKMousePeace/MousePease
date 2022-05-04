using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSmash: CControllerBase
{
    [SerializeField] private float m_AttackSpeed_1 = 1;     //����1 �ӵ�
    //[SerializeField] private float m_AttackSpeed_2 = 1;     //����2 �ӵ�

    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    protected void OnEnable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Animator.SetFloat("AttackSpeed", m_AttackSpeed_1);
        m_Actor.g_Animator.SetTrigger("Throw"); //AttackReady01 ���� �����̾�!

        if (m_Actor.CompareController("MonMovement") || m_Actor.CompareController("MonBite"))
        {
            gameObject.SetActive(false);
            return;
        }

    }
    protected void OnDisable()
    {
        if (m_Actor == null) return;
    }

    private void Update()
    {

    }
}
