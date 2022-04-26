using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CDeadZone : CControllerBase
{
    [SerializeField] private NavMeshAgent m_nav;            //보스
    [SerializeField] private float m_AttackSpeed_1 = 1;     //공격1
    //[SerializeField] private float m_AttackSpeed_2 = 1;     //공격2

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
            m_Actor.g_Animator.SetTrigger("Throw"); //AttackReady01 원래 공격이야!

            StartCoroutine(AttackDelay());
        }
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.FindGameObjectWithTag("Player").SetActive(false);
    }
}
