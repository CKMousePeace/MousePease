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


    private void OnEnable()
    {
        if (m_Actor == null) return;
            m_ColliderChecker.m_ColliderStay += CollisionStay;
        
    }

    private void OnDisable()
    {
        m_ColliderChecker.m_ColliderStay -= CollisionStay;
        
    }
    

    private void CollisionStay(Collision collision)
    {
        
        for (int i = 0; i < collision.contactCount; i++)
        {
            var normal = collision.GetContact(i).normal;
            if (normal.y >= -0.1f && normal.y <= 0.1f)
            {                
                if (m_Actor.g_Rigid.velocity.y <= -m_FallingSpeed)
                    m_Actor.g_Rigid.velocity = new Vector3(m_Actor.g_Rigid.velocity.x, -m_FallingSpeed, m_Actor.g_Rigid.velocity.z);                


            }
        }       
    }
}
