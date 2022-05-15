using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement : CControllerBase
{

    [SerializeField] private float m_fMaxSpeed;
    [SerializeField, Range(0.1f, 1.0f)] private float m_TurnSpeed;
    

    private float m_currentSpeed;
    private float m_DirX, m_DirY;
    private float m_Yaw = 90.0f;
    private bool m_IsInCheese;
    private float m_BuffSpeed;

    [SerializeField] private CColliderChecker m_Checker;
    [SerializeField] private Vector2 m_ChaseTimeRange;

    public float g_currentSpeed => m_currentSpeed;
    public bool g_IsInCheese { get => m_IsInCheese; set { m_IsInCheese = value; }  }
    public float g_fMaxSpeed => m_fMaxSpeed;


    private void OnDisable()
    {
        m_currentSpeed = 0.0f;             
        m_Actor.g_Animator.SetFloat("Walking", 0.0f);
        
    }

    private void Update()
    {
        if (!GameManager.g_isGameStart) return;
        if (PlayerMoveState()) return;
        PlayerMoveKey();        
        
    }
    private void FixedUpdate()
    {
        if (!GameManager.g_isGameStart) return;
        GravityCheck();
        if (PlayerMoveState()) return;
        BuffCheck();
        Movement();
        WallJumpCheck();

        if (!m_Actor.CompareController("WallJump"))
            m_Actor.g_Animator.SetFloat("Walking", m_currentSpeed / m_fMaxSpeed);
    }


    private void WallJumpCheck()
    {
        float WallJumpRayDistance = (m_Checker.g_Collider.bounds.extents.x) * 1.3f;
        Vector3 WallJumpOffsetY = new Vector3(0.0f, -m_Checker.g_Collider.bounds.extents.y * 0.7f, 0.0f);


        RaycastHit hit;
        //WallJump 생성
        if (Physics.Raycast(m_Actor.transform.position + WallJumpOffsetY, m_Actor.transform.forward, out hit, WallJumpRayDistance) ||
            Physics.Raycast(m_Actor.transform.position - WallJumpOffsetY, m_Actor.transform.forward, out hit, WallJumpRayDistance))
        {

            if (hit.transform.CompareTag("Wall"))
            {
                m_Actor.GenerateController("WallJump");
            }

        }
        else
        {
            m_Actor.DestroyController("WallJump");
        }
    }

    private void PlayerMoveKey()
    {
        m_DirX = Input.GetAxisRaw("Horizontal");
        m_DirY = Input.GetAxisRaw("Vertical");
    }
        


    // 달리는 함수입니다.
    private void Movement()
    {
        if (!m_Actor.CompareController("WallJump"))
        {
            PlayerMove();
            TurnRot();
        }
    }
    
    //실질적으로 플레이어 움직이는 함수
    private void PlayerMove()
    {
        if (!m_IsInCheese)
        {
            m_DirY = 0.0f;
        }

        
        var AbsDir = Mathf.Abs(m_DirX);
        var Dir = new Vector3(m_Actor.transform.forward.x * AbsDir, m_DirY, 0.0f).normalized;

        var JumpController = m_Actor.GetController("Jump") as CJump;
        
        if ((m_DirY == 0.0f && m_DirX == 0.0f) || (!JumpController.g_MoveCheck))            
        {
            m_currentSpeed = 0.0f;            
            return;
        }

        if (m_Actor.CompareController("Jump") && JumpController.g_IsWallJumpCheck && !m_Actor.CompareController("WallJump"))
        {
            if (m_Actor.g_Rigid.velocity.x >= 0.2f || m_Actor.g_Rigid.velocity.x <= -0.2f)
            {
                if (m_Actor.g_Rigid.velocity.x < 0.0f && m_DirX > 0.0f || m_Actor.g_Rigid.velocity.x > 0.0f && m_DirX < 0.0f)
                {
                    float velocityx = Mathf.Lerp(m_Actor.g_Rigid.velocity.x, 0.0f, (m_fMaxSpeed * Time.fixedDeltaTime * 5.0f));
                    m_Actor.g_Rigid.velocity = new Vector3(velocityx, m_Actor.g_Rigid.velocity.y, m_Actor.g_Rigid.velocity.z);                    
                    return;
                }
            }
            else
            {
                float velocityx = Mathf.Lerp(m_Actor.g_Rigid.velocity.x, m_fMaxSpeed * m_DirX, (m_fMaxSpeed * Time.fixedDeltaTime * 5.0f));
                m_Actor.g_Rigid.velocity = new Vector3(velocityx, m_Actor.g_Rigid.velocity.y, m_Actor.g_Rigid.velocity.z);                
                return;
            }            
        }


        
        m_currentSpeed = m_fMaxSpeed + m_BuffSpeed;
        var Displacement = Dir * (m_currentSpeed) * Time.fixedDeltaTime;
        m_Actor.g_Rigid.MovePosition(m_Actor.transform.position + Displacement);
    }


    // y축 angle을 변경하는 함수 입니다.
    private void TurnRot()
    {
        if (m_Actor.CompareSkill("DownHill")) return;
        var PlayerEulerAngles = m_Actor.transform.eulerAngles;
        float CurrentAngle = Mathf.LerpAngle(PlayerEulerAngles.y, m_Yaw, m_TurnSpeed);
        m_Actor.g_Rigid.MoveRotation(Quaternion.Euler(new Vector3(PlayerEulerAngles.x, CurrentAngle, PlayerEulerAngles.z)));

        if (m_DirX == 0.0f)
        {            
            return;
        }
        m_Yaw = m_DirX * 90.0f;
    }
   
    private bool PlayerMoveState()
    {
        if (m_Actor.CompareBuff("KnockBack"))
        {
            return true;
        }
        if (m_Actor.CompareController("Dash"))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    ///치즈 관련 부분 
    /// </summary>
    private void GravityCheck()
    {
        if (m_Actor.CompareController("Dash") || m_Actor.CompareSkill("DownHill")) return;
        if (g_IsInCheese)
        {
            m_Actor.g_Rigid.useGravity = false;
        }
        else
        {
            m_Actor.g_Rigid.useGravity = true;
        }
    }

    private void BuffCheck()
    {
        m_BuffSpeed = 0.0f;

        if (m_Actor.CompareBuff("Slow"))
        {
            var SlowBuff = m_Actor.GetBuff("Slow") as CSlow;
            m_BuffSpeed += -(m_fMaxSpeed * (SlowBuff.g_SlowSpeed) * 0.01f);
        }
        if (m_Actor.CompareBuff("Fast"))
        {
            var SlowBuff = m_Actor.GetBuff("Fast") as CFast;
            m_BuffSpeed += (m_fMaxSpeed * (SlowBuff.g_FastSpeed) * 0.01f);
        }
    }


    private void OnDrawGizmos()
    {
        if (m_Actor == null || m_Checker == null) return;

        float WallJumpRayDistance = (m_Checker.g_Collider.bounds.extents.x) * 1.8f;
        Vector3 WallJumpOffsetY = new Vector3(0.0f, -m_Checker.g_Collider.bounds.extents.y * 0.7f, 0.0f);

               


        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_Actor.transform.position + WallJumpOffsetY, m_Actor.transform.position + WallJumpOffsetY + m_Actor.transform.forward * WallJumpRayDistance);
        Gizmos.DrawLine(m_Actor.transform.position - WallJumpOffsetY, m_Actor.transform.position - WallJumpOffsetY + m_Actor.transform.forward * WallJumpRayDistance);

        //WallJump 생성

    }


}
