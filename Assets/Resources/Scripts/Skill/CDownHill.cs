using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDownHill : CSkillBase
{
    [SerializeField]
    private CColliderChecker m_ColliderChecer;



    [SerializeField]
    private float m_StartVelocity;
    [SerializeField]
    private float m_MiddleVelocity;

    [SerializeField]
    private float m_LimitVeiocityY;



    private bool m_isDeceleration;
    private float m_GroundCheckData;

    private Vector3 m_Velocity;
    private float m_CurrnetTime;
    private float m_PlayerSpeed;


    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        m_isDeceleration = false;        
        var PlayerMovemnet = m_Actor.GetController("Movement") as CPlayerMovement;
        m_PlayerSpeed = PlayerMovemnet.g_fMaxSpeed;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (m_Actor == null) return;
        m_GroundCheckData = 5.0f;
        m_isDeceleration = false;
        m_Actor.g_Rigid.useGravity = false;

        var Right = Vector2.right;
        var Left = Vector2.left;


        m_CurrnetTime = 0.0f;
        if (m_Actor.transform.forward.x < 0.0f)
            m_Actor.g_Rigid.velocity = Left * m_PlayerSpeed * 2.0f;
        else if (m_Actor.transform.forward.x > 0.0f)
            m_Actor.g_Rigid.velocity = Right * m_PlayerSpeed * 2.0f;
        else
        {
            gameObject.SetActive(false);
        }
    }


    private void OnDisable()
    {
        m_Actor.g_Rigid.useGravity = false;

        
    }

    private void Update()
    {
        var PlayerMovemnet = m_Actor.GetController("Jump") as CJump;
        if (Input.GetKeyUp(PlayerMovemnet.g_Key))
        {
            gameObject.SetActive(false);
        }

        if (m_Actor.CompareBuff("KnockBack"))
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        
        m_Velocity = m_Actor.g_Rigid.velocity;
        var extentsY = 0.0f;
        if (m_GroundCheckData == 5.0f)
            extentsY = m_ColliderChecer.g_Collider.bounds.extents.y + Mathf.Abs(m_Velocity.y) * 0.5f;
        else
            extentsY = m_ColliderChecer.g_Collider.bounds.extents.y + 1.0f;
        m_CurrnetTime += Time.deltaTime;

        if (Physics.Raycast(m_Actor.transform.position, Vector3.down, extentsY))
        {
            if (!m_isDeceleration)
            {
                m_isDeceleration = true;
                m_GroundCheckData = 1.0f;
                
            }
            else
            {
                gameObject.SetActive(false);
                m_GroundCheckData = 5.0f;
                return;
            }
        }

        if (!m_isDeceleration)
        {
            if (m_CurrnetTime <= 1.0f)
                m_Velocity.y -= m_StartVelocity * Time.deltaTime;

            else
            {
                m_Velocity.y -= m_MiddleVelocity * Time.deltaTime * (m_CurrnetTime - 1);
                if (m_Velocity.y <= -m_LimitVeiocityY)
                    m_Velocity.y = -m_LimitVeiocityY;
            }            
            
        }
        else
        {
            m_Velocity.y += Mathf.Abs(m_Actor.g_Rigid.velocity.y) * Time.deltaTime;

            if (m_Velocity.y >= -1.0f)
                m_Velocity.y = -1.0f;

        }

        Debug.Log(Mathf.Abs(m_Velocity.y));
        m_Actor.g_Rigid.velocity = m_Velocity;        

    }


    
   

}
