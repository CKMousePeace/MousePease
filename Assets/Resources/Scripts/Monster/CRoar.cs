using System.Collections;
using UnityEngine;

public class CRoar : CBossController
{    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    protected void OnEnable()
    {
        if (m_Actor == null) return;

        g_agent.velocity = Vector3.zero;
        m_Actor.g_Animator.SetTrigger("Roar");
        gameObject.SetActive(false);

        if (m_Actor.CompareController("MonBite"))
        {
            gameObject.SetActive(false);
            return;
        }

    }

    protected void OnDisable()
    {
        if (m_Actor == null) return;
    }
}
