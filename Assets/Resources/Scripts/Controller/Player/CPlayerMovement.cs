using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement : CControllerBase
{
        
    [SerializeField] private float m_fSpeed;
    [SerializeField , Range(0.0f ,1.0f)] private float m_turnSpeed;
    [SerializeField] private CColliderChecker m_checker;
    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private KeyCode m_RunKey;
    [SerializeField] private float m_fRunningSpeed;
    [SerializeField] private float m_DecreaseSpeed;

    private float m_PlayerYaw = 90.0f;
    private bool m_isRun = false;
    public float m_currentSpeed;

    private Vector3 m_beforeDir = Vector3.zero;

    private void Update()
    {        
        Movement();
        
    }

    // 달리는 함수입니다.
    public void Movement()
    {
        var DirX = Input.GetAxisRaw("Horizontal");
        Running(DirX);
        if (DirX == 0.0f)
        {
            if (m_currentSpeed == 0.0f)
            {
                m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / (m_fRunningSpeed + m_fSpeed)));                
                return;
            }
        }
        else
        {
            var Dir = transform.forward * Mathf.Abs(DirX);
            m_beforeDir = Dir;
        }
        
        
        
        if (m_Actor.CompareBuff("KnockBack")) return;


        // 3d 게임이지만 게임상 2d로 움직이기 때문에 x값만 사용

        m_beforeDir = m_beforeDir.x * Vector3.right;
        var extents = new Vector3(m_checker.g_Collider.bounds.extents.x * 0.9f, 0.0f, 0.0f);

        TurnRot(DirX);
        if (!m_Actor.CompareController("Dash"))
        {
            //만약 이동중 오브젝트와 충돌시 전에 있던 좌표로 이동 시킵니다. 
            if (!Physics.Raycast(m_Actor.transform.position + (DirX * extents), transform.forward, 0.1f, m_LayerMask))
            {                
                m_Actor.transform.position += m_beforeDir * m_currentSpeed * Time.deltaTime;
                m_Actor.g_Animator.SetFloat("Walking" , Mathf.Abs(m_currentSpeed / (m_fRunningSpeed + m_fSpeed)));                
            }
        }        
    }

    // y축 angle을 변경하는 함수 입니다.
    private void TurnRot(float DirX)
    {
        if (DirX != 0.0f)
            m_PlayerYaw = DirX > 0.0f ? 90.0f : -90.0f;
        
        var transEulerRot = m_Actor.transform.rotation.eulerAngles;
        var ResulRot = Quaternion.Euler(transEulerRot.x, m_PlayerYaw, transEulerRot.z);

        m_Actor.transform.rotation = Quaternion.Lerp(m_Actor.transform.rotation, ResulRot, m_turnSpeed);
    }

    //달리는 함수 입니다. 제거해야됨
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
            if (m_currentSpeed >= resultSpeed)
                m_currentSpeed = resultSpeed;
        }
        else if (m_currentSpeed > resultSpeed)
        {
            if (m_DecreaseSpeed == 0.0f) m_DecreaseSpeed = 1.0f;
            
            m_currentSpeed -= Time.deltaTime * m_DecreaseSpeed;
            
            if (m_currentSpeed <= resultSpeed)
                m_currentSpeed = resultSpeed;
            
        }


    }
}
