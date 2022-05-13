using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CMonJump : CControllerBase
{
    [SerializeField] private NavMeshAgent m_nav;            //º¸½º

    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    protected void OnEnable()
    {
        if (m_Actor == null) return;

        m_nav.velocity = Vector3.zero;
        m_Actor.g_Animator.SetTrigger("Jump");
        m_Actor.g_Animator.SetBool("isGround", false);

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }

    }

    protected void OnDisable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Animator.SetBool("isGround", true);
    }

}
