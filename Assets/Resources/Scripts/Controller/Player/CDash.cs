using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDash : CControllerBase
{
    [SerializeField] private float m_DashForce;
    [SerializeField] private float m_DashTime;   
    [SerializeField] private CColliderChecker m_ColliderChecker;
    [SerializeField] private float m_DelayTime;
    private float m_CurrentDelayTime;
    private bool m_Dash;

    private Vector3 m_Dir;    
    private float m_CurrentTime;    


    public override void init(CDynamicObject actor)
    {

        // ���� ���۽� ������Ʈ�� ���ݴϴ�.
        m_CurrentDelayTime = 0.0f;
        gameObject.SetActive(false);
        base.init(actor);
    }
    private void OnDisable()
    {       
        //���� m_Actor�� ���� ��� ������ ���ݴϴ�.
        if (m_Actor == null) return;
        if (m_Dash ) m_CurrentDelayTime = Time.time;

        m_Actor.g_Rigid.useGravity = true;        
    }

    private void OnEnable()
    {
        float m_DirX = Input.GetAxisRaw("Horizontal");
        float m_DirZ = Input.GetAxisRaw("Vertical");

        //m_DirX == 0�� ��� �������� ���� �����̱� ������ return�� ���ݴϴ�. 
        // �Ǵ� �˹������ ���� ��� ���� 
        if (DashChecker(m_DirX , m_DirZ))       
            return;

        m_Actor.g_Animator.SetTrigger("Dash");
        m_Dash = true;        
        m_Dir = new Vector3(m_DirX, 0.0f, m_DirZ);
        m_Dir.Normalize();
        m_CurrentTime = 0.0f;
        m_Actor.g_Rigid.useGravity = false;
        m_Actor.g_Rigid.velocity = Vector3.zero;     
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
            Physics.Linecast(m_Actor.transform.position - ExtentY * 0.5f, m_Actor.transform.position + MoveData - ExtentY * 0.5f, out hit)
            )
        {
             Debug.Log("�浹" + hit.transform.name);
            m_Actor.transform.position -= m_Actor.transform.forward * 0.1f;
            gameObject.SetActive(false);
            return;
        }


        if (m_CurrentTime >= 1.0f)
        {
            gameObject.SetActive(false);
            return;
        }

        m_Actor.g_Rigid.MovePosition(m_Actor.transform.position + MoveData);        
    }
    private bool DashChecker(float Dirx , float DirZ)
    {
        if ((Dirx == 0 && DirZ == 0))
        {
            gameObject.SetActive(false);
            m_Dash = false;
            return true;
        }
        if (m_Actor.CompareBuff("KnockBack") || (m_CurrentDelayTime != 0 && Time.time - m_CurrentDelayTime <= m_DelayTime))
        {
            gameObject.SetActive(false);
            m_Dash = false;
            return true;
        }        
        return false;

    }
}
