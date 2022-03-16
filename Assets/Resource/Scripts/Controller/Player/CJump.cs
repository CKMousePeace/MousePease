using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJump : CControllerBase
{
    [SerializeField] private float m_fForce;
    [SerializeField] private CColliderChecker m_ColliderChecker;
    [SerializeField] private float m_JumpTime;

    private bool m_isDoubleJump = false;
    private bool m_isJump = false;
    private float m_CurrentJumpTime;
    private float m_beforeJumpTime;
    // Update is called once per frame
    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    

    private void OnEnable()
    {
        if (m_Actor == null) return;

        m_isJump = true;
        m_CurrentJumpTime = 0.0f;
        m_beforeJumpTime = 0.0f;
        m_Actor.g_Animator.SetTrigger("Jump");
        m_Actor.g_Animator.SetBool("isGround" , false);
        m_ColliderChecker.g_Collider.isTrigger = true;

        m_Actor.g_Rigid.AddForce(Vector3.up * 3.0f, ForceMode.Impulse);
        m_ColliderChecker.m_ColliderStay += ColliderStay;
        m_ColliderChecker.m_TriggerEnter += TriggerEnter;

    }

    private void OnDisable()
    {
        if (m_Actor == null) return;

        m_Actor.g_Animator.SetBool("isGround", true);        
        m_isJump = false;
        m_isDoubleJump = false;
        m_ColliderChecker.m_ColliderStay -= ColliderStay;
        m_ColliderChecker.m_TriggerEnter -= TriggerEnter;
    }

    private void Update()
    {
        Jump();
        TriggerCheck();
    }

    private void ColliderStay(Collision collder)
    {
        if (!gameObject.activeInHierarchy) return;
        var extents = new Vector3(0.0f, m_ColliderChecker.g_Collider.bounds.extents.y, 0.0f);

        Debug.DrawRay(m_Actor.transform.position - (extents * 0.9f), Vector3.down * 0.3f);
        if (Physics.Raycast(m_Actor.transform.position - (extents * 0.9f), Vector3.down, 0.3f))
        {
            gameObject.SetActive(false);
        }

    }
    private void TriggerEnter(Collider other)
    {

        if (!other.CompareTag("Floor") && !other.CompareTag("FirstFloor"))
        {
            m_ColliderChecker.g_Collider.isTrigger = false;
        }
    }

    private void Jump()
    {
        if (Input.GetKey(m_Key) && m_CurrentJumpTime <= 1.0f && m_isJump)
        {
            m_CurrentJumpTime += Time.deltaTime / m_JumpTime;
            var deltaTime = m_CurrentJumpTime - m_beforeJumpTime;
            if (m_CurrentJumpTime >= 1.0f)
                deltaTime -= (m_CurrentJumpTime - 1.0f);


            m_Actor.g_Rigid.AddForce(Vector3.up * (m_fForce * deltaTime) * 3.0f, ForceMode.Impulse);
            m_beforeJumpTime = m_CurrentJumpTime;
        }
        else
        {           
            m_isJump = false;
            DoubleJump();
        }        
    }

    private void DoubleJump()
    {        
        if (Input.GetKeyDown(m_Key))
        {
            if (!m_isDoubleJump)
            {
                m_isDoubleJump = true;
                m_Actor.g_Rigid.AddForce(Vector3.up * (m_fForce) * 1.5f, ForceMode.Impulse);
            }
        }
    }
    private void TriggerCheck()
    {
        if ((!m_isJump) && m_ColliderChecker.g_Collider.isTrigger == true && m_Actor.g_Rigid.velocity.y <= -0.1f)
        {
            if (!m_Actor.CompareController("DownJump"))
                m_ColliderChecker.g_Collider.isTrigger = false;
        }
    }

}
