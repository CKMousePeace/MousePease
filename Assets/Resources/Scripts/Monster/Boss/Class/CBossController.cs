using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class CBossController : CControllerBase
{
    private AIBehaviour m_currentBehavior;
    public AIBehaviour CurrentBehavior      //���� ���� 
    {
        get => m_currentBehavior;
        private set
        {
            m_currentBehavior?.Deactivate(this);
            value.Activate(this);
            m_currentBehavior = value;
        }
    }

    private Patrol m_PatrolBehavior;                  //��Ʈ��(��������Ʈ)
    private Investigate m_InvestigateBehavior;        //Ÿ�� �ִ��� �ֺ� ����
    private Chase m_ChaseBehavior;                    //���� �ȿ� ������ �߰�

    //==�׺�ޚ�==//
    [SerializeField]
    private NavMeshAgent m_agent;
    public NavMeshAgent g_agent => m_agent;

    //==�����ٵ�==//
    [SerializeField]
    private Rigidbody m_RigidBoss;
    public Rigidbody g_RigidBoss => m_RigidBoss;

    public float RemainingDistance { get => m_agent.remainingDistance;  }
    /*HoldDown ��ų ����ϸ� ���� �ָ����� �����ٵ� NavMesh ���� �ش� ������ ������ 
    �� �̴� ���ֵ� ������!!*/

    public float StoppingDistance { get => m_agent.stoppingDistance; }
    public void SetDestination(Vector3 destination) => m_agent.SetDestination(destination);

    private float m_defaultAgentSpeed;
    public void MultiplySpeed(float factor) => m_agent.speed = m_defaultAgentSpeed * factor;    //�ӵ� ���
    public void SetDefaultSpeed() => m_agent.speed = m_defaultAgentSpeed;

    private BossEyes eyes;
    private BossEars ears;
    public void IgnoreEars(bool ignore) => ears.gameObject.SetActive(!ignore);      //�÷��̾� �߰ݽ� û�� ��Ȱ��ȭ


    [Header("��Ʈ�ξ� Ȱ��ȭ ����")]
    public bool g_isCheckIntro = false;         //��Ʈ�ξ� Ȱ��ȭ ����



    private void Start()
    {       
        //m_agent = m_Actor.GetComponent<NavMeshAgent>();
        //m_RigidBoss = m_Actor.GetComponent<Rigidbody>();
        GameManager.GameStartEvent();

        if (g_isCheckIntro == true) m_agent.speed = 0;

        m_defaultAgentSpeed = m_agent.speed;
        m_PatrolBehavior = GetComponent<Patrol>();
        m_InvestigateBehavior = GetComponent<Investigate>();
        m_ChaseBehavior = GetComponent<Chase>();

        eyes = GetComponentInChildren<BossEyes>();
        eyes.OnDetect += Chase;
        eyes.OnLost += Investigate;

        ears = GetComponentInChildren<BossEars>();
        ears.OnDetect += Investigate;

        Patrol();
    }

    private void Update()
    {
        BossAnimation();
        CurrentBehavior.UpdateStep(this);

        if (m_agent.enabled == false)
        {
            throw new System.Exception("HoldDown ��ų �����! �����Ƴ�!");
        }

    }
    public void BossIntroStart()
    {    
        StartCoroutine(StartIntro());
    }


    
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Cheese"))
        {
            m_RigidBoss.isKinematic = false;
            m_Actor.g_Animator.SetTrigger("Throw");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        m_RigidBoss.isKinematic = true;
    }


    public void Patrol()        //��������Ʈ
    {
        CurrentBehavior = m_PatrolBehavior;
    }

    public void Investigate(Detectable detectable)      //�ֺ� Ž��
    {
        m_InvestigateBehavior.Destination = detectable.transform.position;
        CurrentBehavior = m_InvestigateBehavior;
    }

    public void Chase(Detectable detectable)        //������ �÷��̾� Ž��. �߰�
    {
        m_ChaseBehavior.g_Target = detectable.transform;
        CurrentBehavior = m_ChaseBehavior;
    }

    public void BossAnimation()        //���� Nav���� �ӵ� �޾ƿ��� ���� �ִϸ����Ϳ� �־���
    {
        float velocity = m_agent.velocity.magnitude;
        m_Actor.g_Animator.SetFloat("Speed", velocity);
    }

    IEnumerator StartIntro()
    {
        GameManager.GameStopEvent();

        m_agent.speed = 0;
        m_Actor.g_Animator.SetTrigger("IntroAnimation");

        yield return new WaitForSeconds(12.0f);

        m_agent.speed = 6;

        yield return new WaitForSeconds(5.0f);

        GameManager.GameStartEvent();

        m_agent.velocity = Vector3.zero;
        m_agent.speed = 0;

        yield return new WaitForSeconds(13.0f);

        yield break;


    }

}