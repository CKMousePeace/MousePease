using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDash : CControllerBase
{
    [SerializeField] private float m_DashForce;
    [SerializeField] private float m_DashTime;   

    private Vector3 m_Dir;    
    private float m_CurrentTime;

    private float m_DelayTime;

    public override void init(CDynamicObject actor)
    {

        // ���� ���۽� ������Ʈ�� ���ݴϴ�.
        m_DelayTime = 0.0f;
        gameObject.SetActive(false);
        base.init(actor);
        
    }
    private void OnDisable()
    {       
        //���� m_Actor�� ���� ��� ������ ���ݴϴ�.
        if (m_Actor == null) return;
        m_Actor.g_Rigid.useGravity = true;        
    }

    private void OnEnable()
    {
        float m_DirX = Input.GetAxisRaw("Horizontal");

        //m_DirX == 0�� ��� �������� ���� �����̱� ������ return�� ���ݴϴ�.
        if (m_DirX == 0 || (m_DelayTime != 0 && Time.time - m_DelayTime  <= 0.3f))
        //if (m_DirX == 0) 
        {
            gameObject.SetActive(false);
            return;
        }
        m_DelayTime = Time.time;
        m_Dir = new Vector3(m_DirX, 0.0f, 0.0f);
        m_Dir.Normalize();
        m_CurrentTime = 0.0f;
        m_Actor.g_Rigid.useGravity = false;
        m_Actor.g_Rigid.velocity = Vector3.zero;
     
    }

    private void Update()
    {
        Dash();
    }

   
    private void Dash()
    {
        
        float DashTime = Time.deltaTime / m_DashTime;        
        m_CurrentTime += DashTime;
        var MoveData = m_Dir * m_DashForce * DashTime;
         
        // �뽬 �� ��� ���̿� ������Ʈ�� ���� ��� �뽬�� ���� �մϴ�.
        if (Physics.Linecast(m_Actor.transform.position, m_Actor.transform.position + MoveData))
        {            
            m_Actor.transform.position -= m_Actor.transform.forward * 0.1f;
            gameObject.SetActive(false);
            return;
        }

        if (m_CurrentTime >= 1.0f)
            gameObject.SetActive(false);

        m_Actor.transform.position += MoveData;        
    }

    

}
