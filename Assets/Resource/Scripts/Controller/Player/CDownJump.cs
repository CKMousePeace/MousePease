using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDownJump : CControllerBase
{
    [SerializeField] private CColliderChecker m_ColliderChecker;
    [SerializeField] private LayerMask m_LayerMask;
    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
        
    }

    private void OnEnable()
    {
        if (m_Actor == null) return;
        var extents = new Vector3(0.0f, m_ColliderChecker.g_Collider.bounds.extents.y, 0.0f);
        m_ColliderChecker.m_TriggerExit += TriggerExit;
        if (Physics.Raycast(m_ColliderChecker.transform.position - (extents * 0.9f), -transform.up, 0.3f, m_LayerMask))
        {
            m_ColliderChecker.g_Collider.isTrigger = true;
            return;
        }
        else
        {
            gameObject.SetActive(false);
            return;
        }
    }
    private void OnDisable()
    {
        if (m_Actor == null) return;
        m_ColliderChecker.m_TriggerExit -= TriggerExit;
    }

    private void TriggerExit(Collider other)
    {
        
        if (gameObject.activeInHierarchy == true)
        {
            m_ColliderChecker.g_Collider.isTrigger = false;
            gameObject.SetActive(false);
        }
    }
}
