using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CJump : CControllerBase
{
    [SerializeField] private float m_fForce;
    [SerializeField] private float m_fDoubleForce;

    [SerializeField] private CColliderChecker m_ColliderChecker;

    [SerializeField]
    private float m_WallJumpPower;

    [SerializeField, Header("다시 점프 할 수 있는 각도를 나타 냅니다.")]
    private float m_MaxGroundAngle; //다시 점프 할 수 있는 각도를 지정합니다.

    [SerializeField]
    private float m_DownhillKeyDownTime;
    [SerializeField]
    private float m_fStamina;



    private float m_CurrentDownHillKeyDownTime;


    private bool m_isDoubleJump = false;
    private bool m_isDoubleJumpCheck = false;
    private bool m_isJump = false;
    private CPlayerMovement m_PlayerMovement;
    

    private bool m_JumpCheck = false;
    public bool g_isDoubleJump => m_isDoubleJump;
    public bool g_MoveCheck { get; set; }
    public bool g_isJump => m_isJump;
    public bool g_JumpCheck => m_JumpCheck;




    private float m_MaxGroundDot;

    private bool TempJump;

    [SerializeField]
    private float m_WallJumpDelay;
    private float m_CurrentWallJumpDelay;

    private float m_EndJumpDistance = 0.5f;
    private float m_DirX;
    private CPlayer m_Player;
    private bool m_DownHillCheck = false;
    

    private bool m_IsWallJump;
    private bool m_IsWallJumpCheck;    
    public bool g_IsWallJumpCheck => m_IsWallJumpCheck;


    public override void init(CDynamicObject actor)
    {
        // 시작하자 마자 오브젝트를 꺼줍니다.
        gameObject.SetActive(false);
        base.init(actor);
        m_Player = actor.GetComponent<CPlayer>();
        m_PlayerMovement = m_Player.GetController("Movement") as CPlayerMovement;
        g_MoveCheck = true;
        m_MaxGroundDot = Mathf.Cos(Mathf.Deg2Rad * m_MaxGroundAngle);

    }

    


    private void OnEnable()
    {
        if (m_Actor == null) return;

        if (m_Actor.CompareController("Dash") || m_Actor.CompareBuff("KnockBack") || m_Player.CompareInCheese())
        {
            gameObject.SetActive(false);
            return;
        }
        if (m_Player.g_fStamina < m_fStamina)
        {
            gameObject.SetActive(false);
            return;
        }

        m_Player.g_fStamina -= m_fStamina;
        m_CurrentDownHillKeyDownTime = 0.0f;
        m_IsWallJumpCheck = false;
        m_DownHillCheck = false;
        
        m_IsWallJump = false;
        g_MoveCheck = true;        
        TempJump = false;
        m_JumpCheck = false;

       
        CObjectPool.g_instance.ObjectPop("PlayerJumpEffect" , m_Actor.transform.position , Quaternion.identity , Vector3.one);
        m_CurrentWallJumpDelay = Time.time - m_WallJumpDelay;

        if (!m_Actor.CompareController("WallJump"))
        {
            m_isJump = true;
        }
        else
        {
            m_IsWallJump = true;
            m_isJump = false;            
        }

        
        //m_Actor.g_Rigid.velocity = new Vector3(m_Actor.g_Rigid.velocity.x, 0.0f, m_Actor.g_Rigid.velocity.y);

        m_Actor.g_Animator.SetBool("isGround", false);
        m_ColliderChecker.m_ColliderStay += ColliderStay;

    }   
    private void OnDisable()
    {
        if (m_Actor == null) return;

        
        m_IsWallJumpCheck = false;
        m_DownHillCheck = false;
        m_IsWallJump = false;
        g_MoveCheck = true;
        m_isJump = false;
        m_JumpCheck = false;
        m_isDoubleJump = false;
        TempJump = false;

        m_ColliderChecker.m_ColliderStay -= ColliderStay;
        if (!m_Actor.CompareBuff("KnockBack"))
            m_Actor.g_Rigid.velocity = Vector3.zero;

        m_Actor.g_Animator.SetBool("isGround", true);
        m_Actor.g_Animator.SetBool("JumpReturn", true);
        m_Actor.DestroyController("WallJump");        
    }

    private void Update()
    {
        m_DirX = Input.GetAxisRaw("Horizontal");

        if (m_Player.CompareInCheese() || m_Actor.CompareBuff("KnockBack"))
        {            
            gameObject.SetActive(false);
            return;
        }


        if (Input.GetKeyDown(m_Key))
        {
            if (m_Actor.CompareController("WallJump") && m_DirX != 0.0f)
            {
                m_IsWallJump = true;
            }
            else if (!m_isJump && !m_JumpCheck && !m_Actor.CompareController("WallJump"))
            {
                m_isJump = true;                
            }
            else if (!m_isDoubleJump && !m_Actor.CompareController("WallJump") && !m_isDoubleJumpCheck)
            {                
                m_isDoubleJump = true;                
            }
            
        }

        if (Input.GetKey(m_Key) && !m_Actor.CompareController("WallJump"))
        {
            m_CurrentDownHillKeyDownTime += Time.deltaTime;
            if (m_CurrentDownHillKeyDownTime > m_DownhillKeyDownTime) 
            {
                if (!m_Player.CompareSkill("DownHill") && !m_DownHillCheck)
                {
                    m_Player.GenerateSkill("DownHill");
                    m_DownHillCheck = true;
                }
                return;

            }
        }
        else
        {
            m_DownHillCheck = false;
            m_CurrentDownHillKeyDownTime = 0.0f;
        }
    }

    private void FixedUpdate()
    {

        if (m_Actor.CompareController("WallJump"))
            m_IsWallJumpCheck = false;
        else 
            m_IsWallJumpCheck = true;

        if (Physics.Raycast(m_Actor.transform.position, -Vector3.up, m_EndJumpDistance) && m_Actor.g_Rigid.velocity.y <= -0.3f)
        {
            if (TempJump == false)
            {
                m_Actor.g_Animator.SetTrigger("JumpReturn");
            }
            TempJump = true;
        }

        WallJump();
        DoubleJump();
        Jump();
    }


    // 일반 점프 사용
    private void Jump()
    {
        var WallJump = m_Actor.GetController("WallJump") as CWallJump;
        if (WallJump == null)
        {
            Debug.LogError("WallJump Controller가 없습니다.");
            return;
        }

        var Dir = Vector3.up;

        if (m_isJump && !m_JumpCheck)
        {
            
            var velocity = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fForce) * Dir;            
            m_Actor.g_Rigid.velocity = velocity;
            if (m_IsWallJumpCheck)
            {
                m_Actor.g_Animator.SetTrigger("Jump");
            }            
            m_isDoubleJumpCheck = false;
            m_JumpCheck = true;
            m_isJump = false;
        }
    }


    //아래에 오브젝트와 충돌 할 경우 오브젝트를 종료 합니다.
    private void ColliderStay(Collision collder)
    {        
        if (m_Actor.g_Rigid.velocity.y <= 0.3f && !m_Actor.CompareController("WallJump"))
        {
            for (int i = 0; i < collder.contactCount; i++)
            {
                var normal = collder.GetContact(i).normal;
                if (normal.y > m_MaxGroundDot)
                {
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }






    //Floor를 제외한 다른 오브젝트와 충돌을 할 경우 trigger를 제거 해줍니다.
    // 더블 점프를 사용 할 수 있는지 판별합니다.
    private void DoubleJump()
    {


        if (!m_isJump && m_isDoubleJump && m_JumpCheck && !m_isDoubleJumpCheck)
        {
            TempJump = false;            
            var forceY = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fDoubleForce);
            
            var Dir = new Vector3(m_DirX * m_PlayerMovement.g_fMaxSpeed, forceY, 0.0f);
            m_Actor.g_Rigid.velocity = Vector3.zero;
            m_Actor.g_Rigid.velocity  = Dir;

            if (m_IsWallJumpCheck)
            {
                m_Actor.g_Animator.SetTrigger("DoubleJump");
            }
            if (m_DirX != 0.0f)
            {
                g_MoveCheck = false;
            }
            m_isDoubleJumpCheck = true;

        }
        else
        {
            if (m_Actor.CompareController("Dash"))
            {
                g_MoveCheck = true;
            }
        }
    }





    //벽 점프 사용
    private void WallJump()
    {

        Debug.Log(Time.time - m_CurrentWallJumpDelay);
        if (Time.time - m_CurrentWallJumpDelay <= m_WallJumpDelay) return;
        if (m_DirX == 0.0f) return;

        if (!m_IsWallJumpCheck && m_IsWallJump)
        {
            
            m_IsWallJumpCheck = true;
            m_IsWallJump = false;

            var force = 0.0f;
            var Dir = Vector3.zero;
            m_Actor.g_Rigid.velocity = Vector3.zero;

            if (!((m_Actor.transform.forward.x < 0.0f && m_DirX < 0.0f) || (m_Actor.transform.forward.x > 0.0f && m_DirX > 0.0f)))
            {
                
                force = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_WallJumpPower);
                Dir = new Vector3(m_DirX, 2.0f, 0.0f).normalized;
                m_Actor.g_Animator.SetTrigger("WallJump");
                Debug.Log(m_DirX.ToString() + " " + transform.forward.x.ToString());
            }
            else
            {
                
                force = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_WallJumpPower);
                m_Actor.g_Animator.SetBool("IsWallJumpNormal" , true);
                Dir = new Vector3(-m_DirX, 5.0f, 0.0f).normalized;
                Debug.Log(m_DirX.ToString() + " " + transform.forward.x.ToString());                
            }
            
            m_Actor.g_Rigid.velocity = force * Dir;
            StartCoroutine(DecreaseVelocityX());
            g_MoveCheck = true;
            m_CurrentWallJumpDelay = Time.time;

        }

    }


    private IEnumerator DecreaseVelocityX()
    {
        yield return new WaitUntil(() =>
        {
            if (!gameObject.activeInHierarchy)
                return true;
            var valocity = m_Actor.g_Rigid.velocity;
            valocity.x = Mathf.Lerp(m_Actor.g_Rigid.velocity.x, 0.0f, Mathf.Abs(m_DirX) * Time.deltaTime);
            m_Actor.g_Rigid.velocity = valocity;

            Debug.Log(valocity);
            if (m_Actor.g_Rigid.velocity.x >= -0.1f && m_Actor.g_Rigid.velocity.x <= 0.1f)
            {
                return true;
            }
            return false;
        });
    }

}