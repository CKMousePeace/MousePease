using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CDynamicObject
{
    private CColliderChecker m_ColliderChecker;
    private float m_OffsetCameraY;
    public float g_OffsetCameraY => m_OffsetCameraY;

    protected override void Start()
    {
        base.Start();
        m_ColliderChecker = gameObject.GetComponent<CColliderChecker>();
        m_ColliderChecker.m_ColliderEnter += ColliderEnter;
    }

    protected void Update()
    {
        if (CompareBuff("KnockBack")) return;

        foreach (var controller in m_ControllerBases)
        {           
            var controllerGameObj = controller.gameObject;
            if (Input.GetKeyDown(controller.g_Key))
            {
                if (controllerGameObj.activeInHierarchy) continue;
                controllerGameObj.SetActive(true);
            }
        }
    }

    private void ColliderEnter(Collision collision)
    {
        m_OffsetCameraY = transform.position.y;
    }
}
