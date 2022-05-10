using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;       //For use the nav agent. nav mesh Agent 사용을 위한 선언

public class CDeadZone : CControllerBase
{
    [SerializeField] private NavMeshAgent m_nav;            //보스
    //[SerializeField] private float m_AttackSpeed_1 = 1;     //공격1

    [SerializeField] private CDynamicObject m_DynamicObject;

    public override void init(CDynamicObject actor)
    {
        //gameObject.SetActive(true);
        base.init(actor);
    }

    private void Start()
    {
        m_nav = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            m_nav.velocity = Vector3.zero;
            m_Actor.g_Animator.SetTrigger("AttackReady01");                      
            StartCoroutine(AttackDelay());
        }

    }

    IEnumerator AttackDelay()
    {
        yield return new WaitUntil(() => {
            bool Check = (m_DynamicObject.g_Animator.GetCurrentAnimatorStateInfo(0).IsName("AttackReady01") &&
            m_DynamicObject.g_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f);
            return Check;
        });
         
        GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>().g_IsDead = true;
    }
    
}
