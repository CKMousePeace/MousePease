using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;       //For use the nav agent. nav mesh Agent ����� ���� ����

public class CDeadZone : CControllerBase
{
    [SerializeField] private NavMeshAgent g_nav;
    [SerializeField] private float g_AttackSpeed_1 = 1;     //����1
    //[SerializeField] private float g_AttackSpeed_2 = 1;     //����2

    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(true);
        base.init(actor);
    }

    private void Start()
    {
        g_nav = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {

            g_nav.velocity = Vector3.zero;
            m_Actor.g_Animator.SetFloat("AttackSpeed", g_AttackSpeed_1);
            m_Actor.g_Animator.SetTrigger("AttackReady01");

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
