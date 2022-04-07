using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement : CControllerBase
{
        
    [SerializeField] private float m_fSpeed;
    [SerializeField , Range(0.0f ,1.0f)] private float m_turnSpeed;
    [SerializeField] private CColliderChecker m_checker;
    [SerializeField] private KeyCode m_RunKey;
    [SerializeField] private float m_fRunningSpeed;
    [SerializeField] private float m_DecreaseSpeed;

    private float m_PlayerYaw = 90.0f;
    private bool m_isRun = false;
    public float m_currentSpeed;
    private float m_DirX;
    private Vector3 m_beforeDir = Vector3.zero;

    private void FixedUpdate()
    {        
        Movement();
        
    }

    private void Update()
    {
        m_DirX = Input.GetAxisRaw("Horizontal");
    }
    // �޸��� �Լ��Դϴ�.
    public void Movement()
    {
        
        Running(m_DirX);
        if (m_DirX == 0.0f)
        {
            if (m_currentSpeed == 0.0f)
            {
                m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / (m_fRunningSpeed + m_fSpeed)));                
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


        // 3d ���������� ���ӻ� 2d�� �����̱� ������ x���� ���
        m_beforeDir = m_beforeDir.x * Vector3.right;
              

        if (!m_Actor.CompareController("Dash"))
        {
                       
            m_Actor.g_Rigid.position += (m_beforeDir * m_currentSpeed * Time.deltaTime);            
            m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / (m_fRunningSpeed + m_fSpeed)));              
            
        }
    }

    // y�� angle�� �����ϴ� �Լ� �Դϴ�.
    private void TurnRot(float DirX)
    {
        if (DirX != 0.0f)
            m_PlayerYaw = DirX > 0.0f ? 90.0f : -90.0f;
        
        var transEulerRot = m_Actor.transform.rotation.eulerAngles;
        var ResulRot = Quaternion.Euler(transEulerRot.x, m_PlayerYaw, transEulerRot.z);

        m_Actor.transform.rotation = Quaternion.Lerp(m_Actor.transform.rotation, ResulRot, m_turnSpeed);
    }

    //�޸��� �Լ� �Դϴ�. �����ؾߵ�
    private void Running(float Dir)
    {

        var resultSpeed = 0.0f;
        if (Dir != 0.0f)
        {
            resultSpeed += m_fSpeed;

        }
        if (m_isRun)
        {
            resultSpeed += m_fRunningSpeed;
        }


        if (Input.GetKey(m_RunKey))
        {
            m_isRun = true;
        }
        else
        {
            m_isRun = false;
        }

        if (m_currentSpeed < resultSpeed)
        {
            m_currentSpeed += resultSpeed * Time.deltaTime;
        }
        if (m_currentSpeed >= resultSpeed)
        {
            //m_currentSpeed = resultSpeed;

            
            if (m_DecreaseSpeed == 0.0f) m_DecreaseSpeed = 1.0f;
            
            m_currentSpeed -= Time.deltaTime * m_DecreaseSpeed;
            
            if (m_currentSpeed <= resultSpeed)
                m_currentSpeed = resultSpeed;
            

        }


    }
}
