using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CThornvine : CStaticObject
{
    
    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var actor = collision.gameObject.GetComponent<CDynamicObject>();
        if (actor == null) return;
        actor.GenerateBuff("KnockBack", m_Buffinfo);
    }
    private void OnTriggerEnter(Collider other)
    {
        var actor = other.gameObject.GetComponent<CDynamicObject>();
        if (actor == null) return;
        actor.GenerateBuff("KnockBack", m_Buffinfo);
    }    
}
