using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWallJump : CControllerBase
{
    [SerializeField] private float m_FallingSpeed;
    [SerializeField] private float m_SecStamina;
    private CPlayer m_Player;

    private float m_fDeltaStamina;

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        m_Player = actor.transform.GetComponent<CPlayer>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (m_Actor == null) return;

        m_fDeltaStamina = m_SecStamina * Time.deltaTime;

        if (m_Player.g_fStamina < m_fDeltaStamina)
        {
            gameObject.SetActive(false);
            return;
        }

        m_Actor.g_Animator.SetTrigger("WallIdle");        
        m_Actor.g_Animator.SetBool("isWallIdle", true);
        m_Actor.g_Rigid.velocity = Vector3.zero;

    }

    private void OnDisable()
    {        
        m_Actor.g_Animator.SetBool("isWallIdle", false);
        m_Actor.g_Animator.SetBool("IsWallJumpNormal", false);                
    }

    private void Update()
    {
        m_fDeltaStamina = m_SecStamina * Time.deltaTime;

        if (m_Player.g_fStamina < m_fDeltaStamina)
        {
            gameObject.SetActive(false);
            return;
        }

        m_Player.g_fStamina -= m_fDeltaStamina;
    }

    private void FixedUpdate()
    {
        if (m_Actor.g_Rigid.velocity.y < -m_FallingSpeed)
            m_Actor.g_Rigid.velocity = new Vector3(m_Actor.g_Rigid.velocity.x, -m_FallingSpeed, m_Actor.g_Rigid.velocity.z);
    }






}
