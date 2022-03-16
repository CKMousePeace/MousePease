using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDash : CControllerBase
{
    [SerializeField] private float m_DashForce;
    [SerializeField] private float m_DashTime;   

    private Vector3 m_Dir;    
    private float m_CurrentTime;

    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
        
    }
    private void OnDisable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Rigid.useGravity = true;        
    }

    private void OnEnable()
    {
        float m_DirX = Input.GetAxisRaw("Horizontal");
               
        if (m_DirX == 0)
        {
            gameObject.SetActive(false);
            return;
        }        
        
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
