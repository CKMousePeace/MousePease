using System.Collections;
using UnityEngine;

public class CMonJump : CBossController
{

    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }


    protected void OnEnable()
    {
        if (m_Actor == null) return;

        g_agent.velocity = Vector3.zero;
        m_Actor.g_Animator.SetTrigger("Jump");

        gameObject.SetActive(false);

    }

    protected void OnDisable()
    {
        if (m_Actor == null) return;
    }


}
