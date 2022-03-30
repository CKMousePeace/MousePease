using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;       //For use the nav agent. nav mesh Agent 사용을 위한 선언 

public class CMonMovement : CControllerBase
{

    [SerializeField] private NavMeshAgent g_nav;
    [SerializeField] private GameObject g_PlayerTarget;
    [SerializeField] private GameObject Walk;
    [SerializeField] private GameObject Idle;


    private void Start()
    {
        g_nav = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
        g_PlayerTarget = GameObject.FindGameObjectWithTag("Player");
    }

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }


    private void Update()
    {

        BossMovement();
       
    }

    private void BossMovement()
    {

        if(g_nav.destination != g_PlayerTarget.transform.position)
        {
            Walk.gameObject.SetActive(true);
            g_nav.SetDestination(g_PlayerTarget.transform.position);         
        }

        else if ()

        else
        {
            Idle.gameObject.SetActive(true);
            g_nav.SetDestination(transform.position);
        }

    }
}
