using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWallJump : CControllerBase
{

    [SerializeField] private CColliderChecker m_ColliderChecker;
    [SerializeField] private float m_FallingSpeed;
    [SerializeField] private bool m_PassPlayer;
    

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        gameObject.SetActive(false);

    }


    private void Update()
    {

        if (m_Actor.g_Rigid.velocity.y <= -m_FallingSpeed)
            m_Actor.g_Rigid.velocity = new Vector3(m_Actor.g_Rigid.velocity.x, -m_FallingSpeed, m_Actor.g_Rigid.velocity.z);
    }



}
