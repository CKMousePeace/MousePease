using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;       //For use the nav agent. nav mesh Agent 사용을 위한 선언

public class CDeadZone : CControllerBase
{
    [SerializeField] private NavMeshAgent m_nav;            //보스
    [SerializeField] private float m_AttackSpeed_1 = 1;     //공격1
    //[SerializeField] private float m_AttackSpeed_2 = 1;     //공격2
    //[SerializeField] private float m_JumpLoop = 1; //점프 루프
    //[SerializeField] private float m_RoarLoop = 1; //포효 루프

    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(true);
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
            m_Actor.g_Animator.SetFloat("AttackSpeed", m_AttackSpeed_1);
            m_Actor.g_Animator.SetTrigger("Throw"); //AttackReady01

            StartCoroutine(AttackDelay());
        }

    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
        //Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

}
