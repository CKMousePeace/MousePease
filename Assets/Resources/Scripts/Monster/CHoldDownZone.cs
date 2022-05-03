using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CHoldDownZone : CStaticObject
{

    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    private bool isChecker = false;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);
    }


    private void OnTriggerEnter(Collider col)
    {
        isChecker = true;

        if (col.CompareTag("Player") && isChecker == true)
        {
            var actor = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CDynamicObject>();
            actor.GenerateBuff("KnockBack", m_Buffinfo);
        }     
    }

    private void OnTriggerExit(Collider col)
    {
        isChecker = false;
    }


}
