using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSliding : CSkillBase
{
    [SerializeField] private float m_MaxSpeed;
    private CPlayerMovement m_PlayerMovement;
    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        m_PlayerMovement = m_Actor.GetController("Movement") as CPlayerMovement;
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_Actor.g_Animator.SetBool("Sliding", true);
    }
    private void OnDisable()
    {
        m_Actor.g_Animator.SetBool("Sliding", false);
    }
    
    
    
    private void FixedUpdate()
    {       

        
        var velocity = Vector3.zero;
        var TempVelocity = new Vector3(m_PlayerMovement.g_NormalX, 0.0f, 0.0f).normalized;
        if (velocity.magnitude < m_MaxSpeed)
        {
            velocity += TempVelocity * m_MaxSpeed * Time.deltaTime * 50.0f;
        }
        
        velocity.y = m_Actor.g_Rigid.velocity.y;
        m_Actor.g_Rigid.velocity = velocity;
    }
}
