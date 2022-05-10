using UnityEngine;

public class CJump : CControllerBase
{
    [SerializeField] private float m_fForce;
    [SerializeField] private float m_fDoubleForce;

    [SerializeField] private CColliderChecker m_ColliderChecker;

    private bool m_isDoubleJump = false;                //플랫폼에서 점프하는지 아닌지 확인하기에 public 으로 변경
    private bool m_isJump = false;       //체크 플랫폼에서 점프하는지 아닌지 확인하기에 public 으로 변경    

    private bool m_JumpCheck = false;
    public bool g_isDoubleJump => m_isDoubleJump; //플랫폼에서 점프하는지 아닌지 확인하기에 public 으로 변경
    public bool g_isJump => m_isJump;       //체크 플랫폼에서 점프하는지 아닌지 확인하기에 public 으로 변경 
                                    

    [SerializeField , Header("다시 점프 할 수 있는 각도를 나타 냅니다.")]
    private float m_MaxGroundAngle; //다시 점프 할 수 있는 각도를 지정합니다.
    private float m_MaxGroundDot;
    private CPlayer m_Player;

    private bool TempJump;


    public override void init(CDynamicObject actor)
    {
        // 시작하자 마자 오브젝트를 꺼줍니다.
        gameObject.SetActive(false);
        base.init(actor);
        m_Player = actor.GetComponent<CPlayer>();
    }

    private void OnValidate()
    {
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

        TempJump = false;
        m_isJump = true;
        m_Actor.g_Rigid.velocity = new Vector3(m_Actor.g_Rigid.velocity.x,  0.0f , m_Actor.g_Rigid.velocity.y);
        m_JumpCheck = true;
        m_Actor.g_Animator.SetTrigger("Jump");
        m_Actor.g_Animator.SetBool("isGround", false);

        m_ColliderChecker.m_ColliderStay += ColliderStay;
    }
    private void OnDisable()
    {
        if (m_Actor == null) return;

        m_isJump = false;
        m_JumpCheck = false;        
        m_Actor.g_Animator.SetBool("isGround", true);        
        m_isDoubleJump = false;
        TempJump = false;

        m_ColliderChecker.m_ColliderEnter -= ColliderStay;
    }

    private void Update()
    {
        

        if (m_Player.CompareInCheese())
        {
            gameObject.SetActive(false);
            return;
        }
        if (Input.GetKeyDown(m_Key) && !m_Actor.CompareBuff("KnockBack"))
        {
            if (!m_isDoubleJump)
            {
                m_isDoubleJump = true;
            }
        }

    }

    private void FixedUpdate()
    {
        DoubleJump();
        Jump();

        if (m_Actor.g_Rigid.velocity.y <= -0.3f)
        {
            if (TempJump == false)
            {
                m_Actor.g_Animator.SetTrigger("JumpReturn");
            }
            TempJump = true;
            
        }
        
    }

    private void Jump()
    {
        if (m_isJump)
        {
            var velocity = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fForce) * Vector3.up;
            m_Actor.g_Rigid.AddForce(velocity, ForceMode.Impulse);
            m_isJump = false;
        }
    }

    //아래에 오브젝트와 충돌 할 경우 오브젝트를 종료 합니다.
    private void ColliderStay(Collision collder)
    {
        if (m_Actor.g_Rigid.velocity.y <= 0.3f)
        {
            for (int i = 0; i < collder.contactCount; i++)
            {
                var normal = collder.GetContact(i).normal;
                if (normal.y > m_MaxGroundDot)
                {
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
    }

    //Floor를 제외한 다른 오브젝트와 충돌을 할 경우 trigger를 제거 해줍니다.
   

    // 더블 점프를 사용 할 수 있는지 판별합니다.
    private void DoubleJump()
    {
        if (!m_isJump && m_isDoubleJump && m_JumpCheck)
        {
            m_JumpCheck = false;            

            var force = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fDoubleForce);
            float m_DirX = Input.GetAxisRaw("Horizontal");

            var Dir = new Vector3(m_DirX, 3.0f, 0.0f).normalized;
            m_Actor.g_Rigid.velocity = Vector3.zero;
            m_Actor.g_Rigid.AddForce(force * Dir, ForceMode.Impulse);
            m_Actor.g_Animator.SetTrigger("DoubleJump");
        }

    }

    
    
}