using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDownJump : CControllerBase
{
    [SerializeField] private CColliderChecker m_ColliderChecker;
    [SerializeField] private LayerMask m_LayerMask; // �ٿ������� ��� �� �� �ִ� ������Ʈ�� �Ǻ��ϱ� ���� LayerMask�Դϴ�.
    public override void init(CDynamicObject actor)
    {
        // Dash�� ���������� ���۽� ������Ʈ�� ���� ������ �ֱ� ������ 
        // �ٷ� ������Ʈ�� ���ݴϴ�.
        gameObject.SetActive(false);
        base.init(actor);
        
    }

    private void OnEnable()
    {
        if (m_Actor == null) return;
        var extents = new Vector3(0.0f, m_ColliderChecker.g_Collider.bounds.extents.y, 0.0f);
        m_ColliderChecker.m_TriggerExit += TriggerExit;
        //�ٿ� ������ ��� �� �� �ִ� ��Ȳ���� �Ǻ��Ͽ� �ٿ� ������ �����մϴ�
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
