using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonJump : CControllerBase
{

    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    protected void OnEnable()
    {
        if (m_Actor == null) return;

        m_Actor.g_Animator.SetTrigger("Jump");
        m_Actor.g_Animator.SetBool("isGround", false);

        if (m_Actor.CompareController("MonMovement"))
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
