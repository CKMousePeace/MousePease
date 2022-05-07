using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerTriggerChecker : CControllerBase
{
    [SerializeField] private CColliderChecker m_ColliderChecker;    

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        m_ColliderChecker.m_TriggerEnter += TriggerEnter;
    }   

    private void TriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CameraTrigger"))
        {
            var CameraTrigger = collider.gameObject.GetComponent<CCameraTriggerBase>();
            if (CameraTrigger == null) return;            

            
        }
    }


}
