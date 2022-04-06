using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;       //For use the nav agent. nav mesh Agent 사용을 위한 선언

public class CDeadZone : CControllerBase
{
    [SerializeField] private NavMeshAgent g_nav;

    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(true);
        base.init(actor);
    }

    private void Start()
    {
        g_nav = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            g_nav.autoBraking = true;
            //g_nav.speed = 0;
            g_nav.velocity = Vector3.zero;
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
