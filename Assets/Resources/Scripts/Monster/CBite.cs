using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBite : CControllerBase
{
    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    protected void OnEnable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Animator.SetTrigger("Bite");

        if (m_Actor.CompareController("MonMovement"))
        {
            gameObject.SetActive(false);
            return;
        }

    }
    protected void OnDisable()
    {
        if (m_Actor == null) return;
    }

    private void Update()
    {

    }
}
