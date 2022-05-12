using UnityEngine;

public class CJump : CControllerBase
{
    [SerializeField] private float m_fForce;
    [SerializeField] private float m_fDoubleForce;

    [SerializeField] private CColliderChecker m_ColliderChecker;

    [SerializeField]
    private float m_WallJumpPower;

    [SerializeField, Header("�ٽ� ���� �� �� �ִ� ������ ��Ÿ ���ϴ�.")]
     private float m_MaxGroundAngle; //�ٽ� ���� �� �� �ִ� ������ �����մϴ�.

    


    private bool m_isDoubleJump = false;                //�÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����
    private bool m_isJump = false;       //üũ �÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����    

    private bool m_JumpCheck = false;
    public bool g_isDoubleJump => m_isDoubleJump; //�÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����
    public bool g_MoveCheck { get; set; }
    public bool g_isJump => m_isJump;       //üũ �÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ���� 
    


    private float m_MaxGroundDot;
    
    private bool TempJump;
    
    private float m_EndJumpDistance = 6.5f;
    private float m_DirX;
    private CPlayer m_Player;


    private bool m_IsWallJump;
    private bool m_IsWallJumpCheck;

    public bool g_IsWallJumpCheck => m_IsWallJumpCheck;

    public override void init(CDynamicObject actor)
    {
        // �������� ���� ������Ʈ�� ���ݴϴ�.
        gameObject.SetActive(false);
        base.init(actor);
        m_Player = actor.GetComponent<CPlayer>();
        g_MoveCheck = true;

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
        m_IsWallJumpCheck = false;
        m_IsWallJump = false;
        g_MoveCheck = true;
        TempJump = false;
        m_isJump = true;
        m_Actor.g_Rigid.velocity = new Vector3(m_Actor.g_Rigid.velocity.x,  0.0f , m_Actor.g_Rigid.velocity.y);
        m_JumpCheck = true;        
        m_Actor.g_Animator.SetBool("isGround", false);        
        m_ColliderChecker.m_ColliderStay += ColliderStay;
        
    }
    private void OnDisable()
    {
        if (m_Actor == null) return;

        m_IsWallJumpCheck = false;
        m_IsWallJump = false;
        g_MoveCheck = true;
        m_isJump = false;
        m_JumpCheck = false;                
        m_isDoubleJump = false;        
        TempJump = false;

        m_ColliderChecker.m_ColliderStay -= ColliderStay;
        m_Actor.g_Rigid.velocity = Vector3.zero;

        m_Actor.g_Animator.SetBool("JumpReturn", true);
        m_Actor.DestroyController("WallJump");        
    }

    private void Update()
    {
        m_DirX = Input.GetAxisRaw("Horizontal");

        if (m_Player.CompareInCheese())
        {
            gameObject.SetActive(false);
            return;
        }
        if (Input.GetKeyDown(m_Key) && !m_Actor.CompareBuff("KnockBack"))
        {

            if (!m_isDoubleJump && !m_Actor.CompareController("WallJump"))
            {
                m_isDoubleJump = true;
                
            }
            else if (m_Actor.CompareController("WallJump") && m_DirX != 0.0f)
            {
                if (!((m_Actor.transform.forward.x < 0.0f && m_DirX < 0.0f) || (m_Actor.transform.forward.x > 0.0f && m_DirX > 0.0f)))
                {
                    m_IsWallJump = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        float WallJumpRayDistance = m_ColliderChecker.g_Collider.bounds.extents.x * 1.3f;

        RaycastHit hit;



        //WallJump ����
        if (Physics.Raycast(m_Actor.transform.position, m_Actor.transform.forward, out hit, WallJumpRayDistance))
        {

            if (hit.transform.CompareTag("Wall"))
            {
                m_Actor.GenerateController("WallJump");
                m_IsWallJumpCheck = false;
            }

        }
        else
        {

            m_IsWallJumpCheck = true;
            m_Actor.DestroyController("WallJump");
        }

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

    private void Jump()
    {
        var WallJump = m_Actor.GetController("WallJump") as CWallJump;
        if (WallJump == null)
        {
            Debug.LogError("WallJump Controller�� �����ϴ�.");
            return;
        }
        
        var Dir = Vector3.up;

        if (m_isJump)
        {         
            var velocity = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fForce) * Dir;
            m_Actor.g_Rigid.AddForce(velocity, ForceMode.Impulse);
            m_Actor.g_Animator.SetTrigger("Jump");
            m_isJump = false;
        }
    }

    //�Ʒ��� ������Ʈ�� �浹 �� ��� ������Ʈ�� ���� �մϴ�.
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
                    break;
                }
            }
        } 
    }



    //Floor�� ������ �ٸ� ������Ʈ�� �浹�� �� ��� trigger�� ���� ���ݴϴ�.
    // ���� ������ ��� �� �� �ִ��� �Ǻ��մϴ�.
    private void DoubleJump()
    {
        if (!m_isJump && m_isDoubleJump && m_JumpCheck)
        {           
            TempJump = false;
            m_JumpCheck = false;            
            var force = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fDoubleForce);            
            var Dir = new Vector3(m_DirX, 2.0f, 0.0f).normalized;
            m_Actor.g_Rigid.velocity = Vector3.zero;
            m_Actor.g_Rigid.AddForce(force * Dir, ForceMode.Impulse);
            m_Actor.g_Animator.SetTrigger("DoubleJump");
            if (m_DirX == 0.0f) return;
            g_MoveCheck = false;
        }
    }


    //�� ���� ���
    private void WallJump()
    {
        if (!m_IsWallJumpCheck && m_IsWallJump)
        {
            Debug.Log("WallJump");
            m_IsWallJumpCheck = true;
            m_IsWallJump = false;
            var force = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_WallJumpPower);
            var Dir = new Vector3(m_DirX, 5.0f, 0.0f).normalized;
            m_Actor.g_Rigid.velocity = Vector3.zero;
            m_Actor.g_Rigid.AddForce(force * Dir, ForceMode.Impulse);
            g_MoveCheck = true;            
        }
    }


   
}