using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWalk : CControllerBase
{
    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    private void Awake()
    {

    }


    protected void OnEnable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Animator.SetTrigger("Walk");

    }
    protected void OnDisable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Animator.SetTrigger("Idle");

    }

    private void Update()
    {
    }



}
