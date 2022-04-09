using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement : CControllerBase
{
        
    [SerializeField] private float m_fSpeed;
    [SerializeField , Range(0.3f ,1.0f)] private float m_turnSpeed;
    [SerializeField] private CColliderChecker m_checker;
    [SerializeField] private KeyCode m_RunKey;    
    [SerializeField] private float m_DecreaseSpeed;
    [SerializeField] private float m_InCreaseSpeed;
    


    private float m_PlayerYaw = 90.0f;    
    [HideInInspector] public float m_currentSpeed;
    private float m_DirX;
    private Vector3 m_beforeDir = Vector3.zero;

    

    private void FixedUpdate()
    {        
        Movement();        
    }
    private void OnDisable()
    {
        m_currentSpeed = 0.0f;
        m_Actor.g_Animator.SetFloat("Walking", 0.0f);

    }


    private void Update()
    {
        m_DirX = Input.GetAxisRaw("Horizontal");
                    
    }
    // 달리는 함수입니다.
    public void Movement()
    {
        
        Running(m_DirX);
        if (m_DirX == 0.0f)
        {
            if (m_currentSpeed == 0.0f)
            {
                m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / m_fSpeed));                
                return;
            }
        }
        else
        {
            var Dir = transform.forward * Mathf.Abs(m_DirX);
            m_beforeDir = Dir;
        }
        
        
       TurnRot(m_DirX);
       if (m_Actor.CompareBuff("KnockBack")) return;


        // 3d 게임이지만 게임상 2d로 움직이기 때문에 x값만 사용
        m_beforeDir = m_beforeDir.x * Vector3.right;
              

        if (!m_Actor.CompareController("Dash"))
        {
                       
            m_Actor.g_Rigid.MovePosition(m_Actor.g_Rigid.position + m_beforeDir * m_currentSpeed * Time.fixedDeltaTime);            
            m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / m_fSpeed));              
            
        }
    }

    // y축 angle을 변경하는 함수 입니다.
    private void TurnRot(float DirX)
    {

        if (DirX != 0.0f)
            m_PlayerYaw = DirX > 0.0f ? 90.0f : 270.0f;
        else
        {
            if (m_Actor.transform.rotation.eulerAngles.y < 180.0f)
                m_PlayerYaw = 90.0f;
            else if (m_Actor.transform.rotation.eulerAngles.y > 180.0f)
                m_PlayerYaw = 270f;            
        }
        var transEulerRot = m_Actor.transform.rotation.eulerAngles;
        var ResultRot = Quaternion.Euler(transEulerRot.x, m_PlayerYaw, transEulerRot.z);

        m_Actor.transform.rotation = Quaternion.Lerp(m_Actor.transform.rotation, ResultRot, m_turnSpeed);
    }


    
    //달리는 함수 입니다. 제거해야됨
    private void Running(float Dir)
    {

        var resultSpeed = 0.0f;
        if (Dir != 0.0f)
            resultSpeed += m_fSpeed;       
        

        if (m_currentSpeed < resultSpeed)
        {
            m_currentSpeed += resultSpeed * Time.fixedDeltaTime * m_InCreaseSpeed;
        }
        if (m_currentSpeed >= resultSpeed)
        {
            //m_currentSpeed = resultSpeed;

            
            if (m_DecreaseSpeed == 0.0f) m_DecreaseSpeed = 1.0f;
            
            m_currentSpeed -= Time.fixedDeltaTime * m_DecreaseSpeed;
            
            if (m_currentSpeed <= resultSpeed)
                m_currentSpeed = resultSpeed;
            

        }


    }
}
