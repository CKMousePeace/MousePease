using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoss : CDynamicObject
{
    private CColliderChecker m_ColliderChecker;
    public float g_Damage = 100;
    //public static  g_MoveSpeed = 7;
    public float g_RunSpeed = 10;



    protected override void Start()
    {
        base.Start();
        m_ColliderChecker = gameObject.GetComponent<CColliderChecker>();
        //m_ColliderChecker.m_ColliderEnter += ColliderEnter;
    }

    protected void Update()
    {
        if (CompareBuff("")) return;

        foreach (var controller in m_ControllerBases)
        {
            //var controllerGameObj = controller.gameObject;
            //if (Input.GetKeyDown(controller.g_Key))
            //{
            //    if (controllerGameObj.activeInHierarchy) continue;
            //    controllerGameObj.SetActive(true);
            //}
        }

    }

    private void ColliderEnter(Collision collision)
    {
        
    }
}
