using UnityEngine;

public class CJump : CControllerBase
{
    [SerializeField] private float m_fForce;
    [SerializeField] private float m_fDoubleForce;

    [SerializeField] private CColliderChecker m_ColliderChecker;

    private bool m_isDoubleJump = false;                //�÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����
    private bool m_isJump = false;       //üũ �÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����    

    private bool m_JumpCheck = false;
    public bool g_isDoubleJump => m_isDoubleJump; //�÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ����
    public bool g_MoveCheck { get; set; }
    public bool g_isJump => m_isJump;       //üũ �÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ���� 
                                    

    [SerializeField , Header("�ٽ� ���� �� �� �ִ� ������ ��Ÿ ���ϴ�.")]
    private float m_MaxGroundAngle; //�ٽ� ���� �� �� �ִ� ������ �����մϴ�.
    private float m_MaxGroundDot;
    private CPlayer m_Player;

    private bool TempJump;
    private float m_EndJumpDistance = 6.5f;


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

        g_MoveCheck = true;
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

        g_MoveCheck = true;
        m_isJump = false;
        m_JumpCheck = false;        
        m_Actor.g_Animator.SetBool("JumpReturn", true);        
        m_isDoubleJump = false;
        TempJump = false;
        m_ColliderChecker.m_ColliderEnter -= ColliderStay;

        m_Actor.g_Rigid.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (m_Actor.CompareController("Dash"))
        {
            g_MoveCheck = true;
        }

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

        RaycastHit hit;
        if (Physics.Raycast(m_Actor.transform.position, -Vector3.up, out hit, m_EndJumpDistance) && m_Actor.g_Rigid.velocity.y <= -0.3f)
        {
            if (TempJump == false)
            {
                m_Actor.g_Animator.SetTrigger("JumpReturn");
                Debug.Log(hit.transform.name);
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
                    return;
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
            float m_DirX = Input.GetAxisRaw("Horizontal");
            var Dir = new Vector3(m_DirX, 2.0f, 0.0f).normalized;

            m_Actor.g_Rigid.velocity = Vector3.zero;
            m_Actor.g_Rigid.AddForce(force * Dir, ForceMode.Impulse);
            m_Actor.g_Animator.SetTrigger("DoubleJump");
            if (m_DirX == 0.0f)
                return ;            
            g_MoveCheck = false;

        }

    }

    



}