using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWallJump : CControllerBase
{
    [SerializeField] private float m_FallingSpeed;

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Animator.SetTrigger("WallIdle");        
        m_Actor.g_Animator.SetBool("isWallIdle", true);
        m_Actor.g_Rigid.velocity = Vector3.zero;
        
        


    }

    private void OnDisable()
    {        
        m_Actor.g_Animator.SetBool("isWallIdle", false);
        m_Actor.g_Animator.SetBool("IsWallJumpNormal", false);                
    }


    private void FixedUpdate()
    {
        if (m_Actor.g_Rigid.velocity.y < -m_FallingSpeed)
            m_Actor.g_Rigid.velocity = new Vector3(m_Actor.g_Rigid.velocity.x, -m_FallingSpeed, m_Actor.g_Rigid.velocity.z);
    }






}
