using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerMovement : CControllerBase
{

    #region 각종 변수들
    [SerializeField] private float m_fMaxSpeed;
    [SerializeField, Range(0.1f, 1.0f)] private float m_TurnSpeed;

    [SerializeField] private float m_EffectTime;
    [SerializeField] private CColliderChecker m_Checker;

    private float m_currentSpeed;
    private float m_DirX, m_DirY;
    private float m_Yaw = 90.0f;
    private bool m_IsInCheese;
    private float m_BuffSpeed;
    private float m_currentVelocity = 2;
    private float m_CurrentLifeTime;
    private float m_NormalX = 0.0f;
    private RaycastHit m_Hit;
    private bool m_isGround;

    public float g_currentSpeed => m_currentSpeed;
    public bool g_IsInCheese { get => m_IsInCheese; set { m_IsInCheese = value; } }
    public bool g_isChase { get; set; }
    public float g_fMaxSpeed { get { return m_fMaxSpeed; } set { m_fMaxSpeed = value; } }

    public float g_ChaseAnimTime = 0.0f;

    public float g_NormalX => m_NormalX;

    private bool g_isWallCheck;

    public RaycastHit g_Hit => m_Hit;

    private bool m_HitCheck;
    #endregion

    private void OnDisable()
    {
        m_currentSpeed = 0.0f;             
        m_Actor.g_Animator.SetFloat("Walking", 0.0f);
        
    }

    #region Update함수
    private void Update()
    {
        
        if (m_Actor.CompareSkill("Sliding") )
        {            
            return;
        }
        m_CurrentLifeTime += Time.deltaTime;
        if (!GameManager.g_isGameStart) return;
        if (PlayerMoveState()) return;
        PlayerMoveKey();

        if (!g_isWallCheck)
        {
            m_Actor.g_Animator.SetFloat("Walking", 0.0f);
            return;
        }
        
    }

    private void FixedUpdate()
    {
        if (!GameManager.g_isGameStart)
        {
            m_Actor.g_Animator.SetFloat("Walking", 0.0f);
            return;
        }
        SlidingCheck();
        g_isWallCheck = BoxCastCheck();
        if (m_Actor.CompareSkill("Sliding") ) return;        
        if (!g_isWallCheck) return;       
        if (PlayerMoveState()) return;
        MovementEffect();
        BuffCheck();
        Movement();
        ChaseWalking();

    }

    #endregion

    #region 슬라이드 부분
    private void SlidingCheck()
    {
        float WallJumpRayDistance = (m_Checker.g_Collider.bounds.extents.y);
        Vector3 ExtentsY = new Vector3(0.0f, WallJumpRayDistance, 0.0f);
        m_HitCheck = Physics.Raycast(m_Actor.transform.position - ExtentsY, -m_Actor.transform.up, out m_Hit, 0.5f);


        if (m_HitCheck)
        {
            m_NormalX = m_Hit.normal.x;
            if (m_Hit.transform.CompareTag("SlidingTile"))
            {
                if (!m_Actor.CompareSkill("Sliding"))
                    m_Actor.GenerateSkill("Sliding");
            }
            else
            {
                if (m_Actor.CompareSkill("Sliding"))
                {
                    if (!m_Actor.DestroySkill("Sliding"))
                    {
                        Debug.LogError("Sliding 가 없습니다.");
                    }
                }
            }
        }
        else
        {
            if (m_Actor.CompareSkill("Sliding"))
            {
                if (!m_Actor.DestroySkill("Sliding"))
                {
                    Debug.LogError("Sliding 가 없습니다.");
                }
            }
        }
    }

    #endregion


    #region 쫒기는 코드
    private void ChaseWalking()
    {
        if (!m_Actor.CompareController("WallJump"))
        {
            if (m_DirX == 0.0f)
            {
                g_ChaseAnimTime = 0.0f;
                m_Actor.g_Animator.SetFloat("Walking", g_ChaseAnimTime);
            }
            else if (!g_isChase)
            {
                g_ChaseAnimTime = Mathf.SmoothDamp(g_ChaseAnimTime, 0.5f, ref m_currentVelocity, 0.1f);
                m_Actor.g_Animator.SetFloat("Walking", g_ChaseAnimTime);
            }
            else
            {
                if (g_ChaseAnimTime == 0.0f)
                {
                    m_Actor.g_Animator.SetFloat("Walking", 1.0f);
                }
                else
                {
                    g_ChaseAnimTime = Mathf.SmoothDamp(g_ChaseAnimTime, 1.0f, ref m_currentVelocity, 0.1f);
                    m_Actor.g_Animator.SetFloat("Walking", g_ChaseAnimTime);
                }
            }
        }



    }
    #endregion


    #region MoveKey입력
    private void PlayerMoveKey()
    {
        m_DirX = Input.GetAxisRaw("Horizontal");
        m_DirY = Input.GetAxisRaw("Vertical");
    }
    #endregion

    //실질적으로 플레이어 움직이는 함수 들

    #region 실제로 달리는 함수
    private void Movement()
    {
        if (!m_Actor.CompareController("WallJump"))
        {
            PlayerMove();
            TurnRot();
        }
    }


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
    #endregion

    #region 달리는 이펙트 함수
    private void MovementEffect()
    {
        float WallJumpRayDistance = (m_Checker.g_Collider.bounds.extents.y) * 1.2f;
        Vector3 WallJumpOffsetY = new Vector3(0.0f, -m_Checker.g_Collider.bounds.extents.y * 0.7f, 0.0f);

        if (m_DirX != 0.0f)
        {
            if (Physics.Raycast(m_Actor.transform.position + WallJumpOffsetY, -m_Actor.transform.up, WallJumpRayDistance))
            {
                m_isGround = true;
                if (m_CurrentLifeTime > m_EffectTime)
                {
                    float PlayerbounsY = (m_Checker.g_Collider.bounds.extents.y) * 0.9f;
                    var OffsetY = new Vector3(0.0f, PlayerbounsY, 0.0f);
                    var Rotation = m_Actor.transform.rotation;

                    CObjectPool.g_instance.ObjectPop("PlayerRunEffect", m_Actor.transform.position - OffsetY, Quaternion.Euler(-90.0f, Rotation.eulerAngles.y - 90.0f, 0.0f), Vector3.one);
                    m_CurrentLifeTime = 0.0f;
                }
            }
            else
            {
                m_isGround = false;
            }
        }
    }
    #endregion

    #region 앞에 물체 있는지 확인 하는 함수
    private bool BoxCastCheck()
    {
        Vector3 velocity = m_Actor.g_Rigid.velocity;

        if (Physics.BoxCast(m_Checker.g_Collider.bounds.center, m_Checker.g_Collider.bounds.extents * 0.7f, transform.forward, out m_Hit, m_Actor.transform.rotation, 0.2f))
        {

            var PointDir = m_Actor.transform.forward.x;
            var CeilDir = m_DirX;

            if ((m_Actor.CompareController("Jump") || !m_isGround) && m_Hit.transform.CompareTag("Wall"))
            {
                if (!m_Actor.CompareController("WallJump"))
                {
                    m_Actor.GenerateController("WallJump");
                }
            }

            if (m_Hit.collider.isTrigger)
            {
                return true;
            }
            if ((CeilDir > 0.0f && PointDir > 0.0f) || (CeilDir < 0.0f && PointDir < 0.0f))
            {
                velocity.x = 0.0f;
                m_Actor.g_Rigid.velocity = velocity;
                return false;
            }
        }
        else
        {
            m_Actor.DestroyController("WallJump");
        }

        return true;
    }
    #endregion

    #region 버프 확인 하는 함수
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
    #endregion

    #region 플레이어가 움직일 수 있는지 확인 하는 함수
    private bool PlayerMoveState()
    {
        if (m_Actor.CompareBuff("KnockBack"))
        {
            return true;
        }
        else if (m_Actor.CompareController("Dash"))
        {
            return true;
        }
        else if (m_Actor.CompareSkill("DownHill"))
        {
            return true;
        }
        return false;
    }
    #endregion
}
