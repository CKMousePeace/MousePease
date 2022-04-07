using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJump : CControllerBase
{
    [SerializeField] private float m_fForce;
    [SerializeField] private float m_fDoubleForce;

    [SerializeField] private CColliderChecker m_ColliderChecker;    

    private bool m_isDoubleJump = false;                //�÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����
    private bool m_isJump = false;       //üũ �÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����

    public bool g_isDoubleJump => m_isDoubleJump; //�÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����
    public bool g_isJump => m_isJump;       //üũ �÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����    
    private Vector3 m_StartVelocity;

    public override void init(CDynamicObject actor)
    {
        // �������� ���� ������Ʈ�� ���ݴϴ�.
        gameObject.SetActive(false);
        base.init(actor);
    }

    

    private void OnEnable()
    {
        if (m_Actor == null) return;
        if (m_Actor.CompareController("Dash") || m_Actor.CompareController("KnockBack"))
        {
            gameObject.SetActive(false);
            return;
        }



        
        m_isJump = true;
        m_ColliderChecker.g_Collider.isTrigger = true;
        m_ColliderChecker.m_ColliderStay += ColliderStay;
        m_ColliderChecker.m_TriggerEnter += TriggerEnter;
        m_StartVelocity = m_Actor.g_Rigid.velocity;
        m_Actor.g_Animator.SetTrigger("Jump");
        m_Actor.g_Animator.SetBool("isGround" , false);
        m_Actor.g_Rigid.AddForce(Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fForce) * Vector3.up, ForceMode.Impulse);

    }
    private void OnDisable()
    {
        if (m_Actor == null) return;

        m_ColliderChecker.g_Collider.isTrigger = false;
        m_Actor.g_Animator.SetBool("isGround", true);        
        m_isJump = false;
        m_isDoubleJump = false;
        m_ColliderChecker.m_ColliderStay -= ColliderStay;
        m_ColliderChecker.m_TriggerEnter -= TriggerEnter;
        m_isJump = false;
    }

    private void Update()
    {        
        if (m_Actor.CompareController("Dash"))
        {
            gameObject.SetActive(false);
            return;
        }
        Jump();
        TriggerCheck();



    }

    



    //�Ʒ��� ������Ʈ�� �浹 �� ��� ������Ʈ�� ���� �մϴ�.
    private void ColliderStay(Collision collder)
    {        
        var extentsy = new Vector3(0.0f, m_ColliderChecker.g_Collider.bounds.extents.y, 0.0f);
        var extentsx = new Vector3(m_ColliderChecker.g_Collider.bounds.extents.x, 0.0f, 0.0f);

        Debug.DrawRay(m_Actor.transform.position - (extentsy * 0.9f), Vector3.down * 0.5f);
        Debug.DrawRay(m_Actor.transform.position - (extentsy * 0.9f + extentsx), Vector3.down * 0.5f); 
        Debug.DrawRay(m_Actor.transform.position - (extentsy * 0.9f - extentsx), Vector3.down * 0.5f);

        if (Physics.Raycast(m_Actor.transform.position - (extentsy * 0.9f), Vector3.down, 0.5f) || 
            Physics.Raycast(m_Actor.transform.position - (extentsy * 0.9f + extentsx), Vector3.down, 0.5f) ||
            Physics.Raycast(m_Actor.transform.position - (extentsy * 0.9f - extentsx), Vector3.down, 0.5f))
        {
            gameObject.SetActive(false);
        }

    }
    //Floor�� ������ �ٸ� ������Ʈ�� �浹�� �� ��� trigger�� ���� ���ݴϴ�.
    private void TriggerEnter(Collider other)
    {
        if (!other.CompareTag("Floor") && !other.CompareTag("FirstFloor"))
        {
             m_ColliderChecker.g_Collider.isTrigger = false;
        }
    }


    //���� �����ϴ� �Լ� �Դϴ�.
    private void Jump()
    {        
        DoubleJump();        
    }


    // ���� ������ ��� �� �� �ִ��� �Ǻ��մϴ�.
    private void DoubleJump()
    {        
        if (Input.GetKeyDown(m_Key))
        {
            if (!m_isDoubleJump)
            {
                m_isDoubleJump = true;
                m_Actor.g_Rigid.AddForce(Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fDoubleForce) * Vector3.up, ForceMode.Impulse);
            }
        }
    }

    // trigger�� ų�� ���� ���ϴ� �Լ� �Դϴ�.
    private void TriggerCheck()
    {
        
        if (m_ColliderChecker.g_Collider.isTrigger == true && m_Actor.g_Rigid.velocity.y < m_StartVelocity.y)
        {
            m_ColliderChecker.g_Collider.isTrigger = false;
        }
    }

}




/*
 
    private float m_CurrentJumpTime;
    private float m_beforeJumpTime;
    
 * //if (Input.GetKey(m_Key) && m_CurrentJumpTime <= 1.0f && m_isJump)
        //{
        //    m_CurrentJumpTime += Time.deltaTime / m_JumpTime;
        //    var deltaTime = m_CurrentJumpTime - m_beforeJumpTime;
        //    if (m_CurrentJumpTime >= 1.0f)
        //        deltaTime -= (m_CurrentJumpTime - 1.0f);
        //
        //
        //    m_Actor.g_Rigid.AddForce(Vector3.up * (m_fForce * deltaTime) * 3.0f, ForceMode.Impulse);
        //    m_beforeJumpTime = m_CurrentJumpTime;
        //}
        //else
        //{           
            //m_isJump = false;
            DoubleJump();
        //}        
 */