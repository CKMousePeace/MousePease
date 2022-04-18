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
    public bool g_isJump => m_isJump;       //üũ �÷������� �����ϴ��� �ƴ��� Ȯ���ϱ⿡ public ���� ���� 
                                            //�ٽ� ���� �� �� �ִ� ������ �����մϴ�.

    [SerializeField , Header("�ٽ� ���� �� �� �ִ� ������ ��Ÿ ���ϴ�.")]
    private float m_MaxGroundAngle; 
    private float m_MaxGroundDot;

    private bool m_TriggerChecker;
    private CPlayer m_player;


    public override void init(CDynamicObject actor)
    {
        // �������� ���� ������Ʈ�� ���ݴϴ�.
        gameObject.SetActive(false);
        base.init(actor);
        m_player = m_Actor as CPlayer;
    }

    private void OnValidate()
    {
        m_MaxGroundDot = Mathf.Cos(Mathf.Deg2Rad * m_MaxGroundAngle);
    }


    private void OnEnable()
    {
        if (m_Actor == null) return;
        if (m_Actor.CompareController("Dash") || m_Actor.CompareController("KnockBack"))
        {
            gameObject.SetActive(false);
            return;
        }


        m_TriggerChecker = false;
        m_Actor.g_Rigid.velocity = new Vector3(m_Actor.g_Rigid.velocity.x,  0.0f , m_Actor.g_Rigid.velocity.y);
        m_isJump = true;
        m_JumpCheck = true;
        m_ColliderChecker.g_Collider.isTrigger = true;

        m_Actor.g_Animator.SetTrigger("Jump");
        m_Actor.g_Animator.SetBool("isGround", false);

        m_ColliderChecker.m_ColliderStay += ColliderStay;
        m_ColliderChecker.m_TriggerEnter += TriggerEnter;
        m_ColliderChecker.m_TriggerExit += TriggerExit;

    }
    private void OnDisable()
    {
        if (m_Actor == null) return;

        m_JumpCheck = false;
        m_TriggerChecker = false;
        m_ColliderChecker.g_Collider.isTrigger = false;
        m_Actor.g_Animator.SetBool("isGround", true);        
        m_isDoubleJump = false;        

        m_ColliderChecker.m_ColliderStay -= ColliderStay;
        m_ColliderChecker.m_TriggerEnter -= TriggerEnter;
        m_ColliderChecker.m_TriggerExit -= TriggerExit;
    }

    private void Update()
    {
        if (m_Actor.CompareController("Dash") || m_player.MagnetSkillCheck())
        {
            gameObject.SetActive(false);
            return;
        }
        if (Input.GetKeyDown(m_Key))
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
        if (m_isJump)
        {
            var velocity = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fForce) * Vector3.up;
            m_Actor.g_Rigid.velocity += velocity;
            m_isJump = false;
        }

        TriggerCheck();
    }

    //�Ʒ��� ������Ʈ�� �浹 �� ��� ������Ʈ�� ���� �մϴ�.
    private void ColliderStay(Collision collder)
    {
        for (int i = 0; i < collder.contactCount; i++)
        {
            var normal = collder.GetContact(i).normal;            
            if (normal.y > m_MaxGroundDot)
            {
                gameObject.SetActive(false);
            }
            else if (normal.y < 0.0f)
            {
                if (m_Actor.g_Rigid.velocity.y > 0f)
                    m_Actor.g_Rigid.velocity = new Vector3(m_Actor.g_Rigid.velocity.x, 0.0f, m_Actor.g_Rigid.velocity.z);
            }
        }
    }

    //Floor�� ������ �ٸ� ������Ʈ�� �浹�� �� ��� trigger�� ���� ���ݴϴ�.
    private void TriggerEnter(Collider other)
    {
        if (!other.CompareTag("Floor") && !other.CompareTag("FirstFloor"))
        {
            m_ColliderChecker.g_Collider.isTrigger = false;
        }
        else
        {
            m_TriggerChecker = true;
        }
    }
    private void TriggerExit(Collider other)
    {
        if (!other.CompareTag("Floor") && !other.CompareTag("FirstFloor"))
        {
            m_ColliderChecker.g_Collider.isTrigger = false;            
        }
        else 
        {
            m_TriggerChecker = false;
        }
    }

    // ���� ������ ��� �� �� �ִ��� �Ǻ��մϴ�.
    private void DoubleJump()
    {
        
        if (m_isJump == false && m_isDoubleJump && m_JumpCheck)
        {
            m_JumpCheck = false;
            m_isDoubleJump = false;

            var force = Mathf.Sqrt(-2.0f * Physics.gravity.y * m_fDoubleForce);
            if (m_Actor.g_Rigid.velocity.y > 0)
            {
                force = Mathf.Max(0, force - m_Actor.g_Rigid.velocity.y);
            }
            float m_DirX = Input.GetAxisRaw("Horizontal");
            float m_DirZ = Input.GetAxisRaw("Vertical");

            var Dir = new Vector3(m_DirX, 1.0f, m_DirZ).normalized;
            m_Actor.g_Rigid.velocity = Vector3.zero;
            m_Actor.g_Rigid.AddForce(force * Dir, ForceMode.Impulse);
            m_Actor.g_Animator.SetTrigger("DoubleJump");
        }
    }

    // trigger�� ų�� ���� ���ϴ� �Լ� �Դϴ�.
    private void TriggerCheck()
    {

        if (m_ColliderChecker.g_Collider.isTrigger == true && m_Actor.g_Rigid.velocity.y < 0.0f && !m_TriggerChecker)
        {
            //m_TriggerChecker = false;
            m_ColliderChecker.g_Collider.isTrigger = false;
        }
    }
}