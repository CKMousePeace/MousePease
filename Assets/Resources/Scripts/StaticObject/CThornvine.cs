using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CThornvine : CStaticObject
{
    
    [Tooltip("Float[0] = Force, Float[1] = Damage , Float[2] = ���� �ð�")]
    [SerializeField] private CBuffBase.BuffInfo m_KnockBackBuffinfo;

    [Tooltip("Float[0] = ���� �ð�")]
    [SerializeField] private CBuffBase.BuffInfo m_invincibilityBuffinfo;

    private float m_InvincibilityBuffDefulatTime = 3.0f;
    protected override void Start()
    {
        base.Start();
        if (m_invincibilityBuffinfo.g_Value_Float.Count <= 0)
            m_invincibilityBuffinfo.g_Value_Float.Add(m_InvincibilityBuffDefulatTime);

        else if (m_invincibilityBuffinfo.g_Value_Float[0] == 0.0f)
            m_invincibilityBuffinfo.g_Value_Float[0] = m_InvincibilityBuffDefulatTime;

        m_KnockBackBuffinfo.g_Value_Vector3.Add(transform.position);
    }

    private void OnCollisionStay(Collision collision)
    {
        var actor = collision.gameObject.GetComponent<CDynamicObject>();
        if (actor == null || CrushChecker(actor)) return;

        // ���� ������Ʈ���� ���¸� ���� ���ݴϴ�.
        actor.GenerateBuff("KnockBack", m_KnockBackBuffinfo);
        actor.GenerateBuff("Invincibility", m_invincibilityBuffinfo);
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        var actor = other.gameObject.GetComponent<CDynamicObject>();
        if (actor == null) return;
        // ���� ������Ʈ���� ���¸� ���� ���ݴϴ�.
        actor.GenerateBuff("KnockBack", m_Buffinfo);
    } 
    */
}
