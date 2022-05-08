using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CBiteZone : CStaticObject
{

    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    [SerializeField] private GameObject n_MonBite;       //���� ���� �ִϸ� �κ�
    private bool isChecker = false;

    [SerializeField] private int m_SkillRunningTime = 0;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);

        
    }

    private void OnEnable()
    {
        StartCoroutine(StartBite(m_SkillRunningTime));
    }


    private void OnTriggerEnter(Collider col)
    {
        isChecker = true;

        if (col.CompareTag("Player") && isChecker == true)
        {
            n_MonBite.SetActive(true);        //Boss Bite Motion Enable
            var actor = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CDynamicObject>();
            actor.GenerateBuff("KnockBack", m_Buffinfo);
        }     
    }

    private void OnTriggerExit(Collider col)
    {
        isChecker = false;
    }


    IEnumerator StartBite( int Time )
    {
        yield return new WaitForSeconds(Time);
        gameObject.SetActive(false);

    }


}
