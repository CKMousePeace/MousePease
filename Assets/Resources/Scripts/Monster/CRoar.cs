using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRoar : CControllerBase
{
    protected void OnEnable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Animator.SetTrigger("Roar");

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
