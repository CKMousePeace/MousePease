using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CBiteZone : CStaticObject
{

    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    [SerializeField] GameObject MonBite;       //몬스터 물기 애니메 부분
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
            MonBite.SetActive(true);        //Boss Bite Motion Enable
            var actor = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CDynamicObject>();
            actor.GenerateBuff("KnockBack", m_Buffinfo);
        }     
    }

    private void OnTriggerExit(Collider col)
    {
        isChecker = false;
    }


}
