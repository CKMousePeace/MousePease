using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CBiteZone : CStaticObject
{
    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    private bool isChecker = false;     //플레이어 감지
    [SerializeField] private int m_SkillRunningTime = 0;    //스킬 작동 시간
    [SerializeField] private NavMeshAgent BossNav;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);   
    }

    private void OnEnable()
    {
        StartCoroutine(StartBite(m_SkillRunningTime));
    }

    private void OnDisable()
    {
        return;
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


    IEnumerator StartBite( int Time )
    {
        while (true)
        {
            BossNav.speed = 10.0f;

            yield return new WaitForSeconds(Time + 1);

            BossNav.speed = 6.0f;

            gameObject.SetActive(false);
            yield break;

        }
    }
}
