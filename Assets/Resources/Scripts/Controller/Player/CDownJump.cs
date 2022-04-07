using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDownJump : CControllerBase
{
    [SerializeField] private CColliderChecker m_ColliderChecker;
    [SerializeField] private LayerMask m_LayerMask; // 다운점프를 사용 할 수 있는 오브젝트를 판별하기 위한 LayerMask입니다.
    public override void init(CDynamicObject actor)
    {
        // Dash랑 마찬가지로 시작시 오브젝트가 켜져 있을수 있기 때문에 
        // 바로 오브젝트를 꺼줍니다.
        gameObject.SetActive(false);
        base.init(actor);
        
    }

    private void OnEnable()
    {
        if (m_Actor == null) return;
        var extents = new Vector3(0.0f, m_ColliderChecker.g_Collider.bounds.extents.y, 0.0f);
        m_ColliderChecker.m_TriggerExit += TriggerExit;
        //다운 점프를 사용 할 수 있는 상황인지 판별하여 다운 점프를 실행합니다
        RaycastHit hit;
        if (Physics.Raycast(m_ColliderChecker.transform.position - (extents * 0.9f), -transform.up, out hit, 0.3f, m_LayerMask))
        {
            Debug.Log(hit.transform.name);
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
        m_ColliderChecker.g_Collider.isTrigger = false;
        gameObject.SetActive(false);

    }
}
