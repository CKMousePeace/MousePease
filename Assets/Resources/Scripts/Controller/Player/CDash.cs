using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDash : CControllerBase
{

    [SerializeField] private float m_DashForce;
    [SerializeField] private float m_DashTime;   
    [SerializeField] private CColliderChecker m_ColliderChecker;
    [SerializeField] private float m_DashCool;
    [SerializeField] private float m_DashStamina;
    private float m_CurrentDelayTime;
    
    

    private Vector3 m_Dir;    
    private float m_CurrentTime;
    private CPlayer m_Player;    

    

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
        // ���� ���۽� ������Ʈ�� ���ݴϴ�.
        m_CurrentDelayTime = Time.time - m_DashCool;
        gameObject.SetActive(false);        
        m_Player = actor.GetComponent<CPlayer>();
        
    }

    private void OnEnable()
    {

        //m_DirX == 0�� ��� �������� ���� �����̱� ������ return�� ���ݴϴ�. 
        // �Ǵ� �˹������ ���� ��� ���� 
        

        float m_DirX = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
        float m_DirY = Input.GetAxisRaw("Vertical");

        if (m_Player == null || DashChecker(m_DirX, m_DirY)) return;

        if (!m_Player.CompareInCheese())
        {
            m_DirY = 0.0f;
        }

        CObjectPool.g_instance.ObjectPop("PlayerDashEffect", m_Actor.transform.position , Quaternion.Euler(-90.0f , m_Actor.transform.eulerAngles.y - 90.0f , 0.0f) , Vector3.one);
        m_Actor.g_Animator.SetTrigger("Dash");        
        
        m_Dir = new Vector3(m_Actor.transform.forward.x * m_DirX, m_DirY, 0.0f).normalized;
        m_Dir.Normalize();
        m_Player.g_fStamina -= m_DashStamina;
        m_Actor.g_Rigid.useGravity = false;
        m_Actor.g_Rigid.velocity = Vector3.zero;        
        m_CurrentTime = 0.0f;
    }


    private void OnDisable()
    {
        //���� m_Actor�� ���� ��� ������ ���ݴϴ�.
        if (m_Actor == null) return;
        m_Actor.g_Rigid.useGravity = true;
    }


    private void FixedUpdate()
    {
        Dash();
    }

   
    private void Dash()
    {       

        // �ð� üũ
        float DashTime = Time.fixedDeltaTime / m_DashTime;        
        m_CurrentTime += DashTime;
        var MoveData = m_Dir * m_DashForce * DashTime;        
        var Extents = m_ColliderChecker.g_Collider.bounds.extents;
        var ExtentY = new Vector3(0.0f, Extents.y, 0.0f);

        RaycastHit hit;

        if (Physics.Linecast(m_Actor.transform.position + ExtentY * 0.5f , m_Actor.transform.position + MoveData + ExtentY * 0.5f, out hit) ||
            Physics.Linecast(m_Actor.transform.position - ExtentY * 0.5f, m_Actor.transform.position + MoveData - ExtentY * 0.5f, out hit))
        {

            if (!hit.collider.CompareTag("Cheese") && !hit.collider.CompareTag("Volume"))
            {
                Debug.Log("�浹" + hit.transform.name);
                m_Actor.g_Rigid.position -= m_Actor.transform.forward * 0.1f;
                gameObject.SetActive(false);
                return;
            }
        }


        if (m_CurrentTime >= 1.0f)
        {
            gameObject.SetActive(false);
            return;
        }

        m_Actor.g_Rigid.MovePosition(m_Actor.transform.position + MoveData);        
    }


    private bool DashChecker(float Dirx , float DirY)
    {
        if (m_Player.g_fStamina < m_DashStamina)
        {
            gameObject.SetActive(false);
            return true;
        }
        else if (m_Player.CompareInCheese())
        {
            if (Dirx == 0.0f && DirY == 0.0f)
            {
                gameObject.SetActive(false);
                return true;
            }
        }
        else
        {
            if (Dirx == 0)
            {
                gameObject.SetActive(false);
                return true;
            }
        }         
        
        if (m_Actor.CompareBuff("KnockBack"))
        {
            if (m_CurrentDelayTime != 0)
            {
                gameObject.SetActive(false);
                return true;
            }
        }
        if (m_Actor.CompareController("WallJump"))
        {
            gameObject.SetActive(false);
            return true;
        }

        m_CurrentDelayTime = Time.time;
        return false;
    }
}
