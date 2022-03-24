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

        // 게임 시작시 오브젝트를 꺼줍니다.
        m_DelayTime = 0.0f;
        gameObject.SetActive(false);
        base.init(actor);
        
    }
    private void OnDisable()
    {       
        //만약 m_Actor가 없을 경우 리턴을 해줍니다.
        if (m_Actor == null) return;
        m_Actor.g_Rigid.useGravity = true;        
    }

    private void OnEnable()
    {
        float m_DirX = Input.GetAxisRaw("Horizontal");

        //m_DirX == 0일 경우 움직이지 않은 상태이기 때문에 return을 해줍니다.
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
         
        // 대쉬 할 경우 사이에 오브젝트가 있을 경우 대쉬를 종료 합니다.
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
