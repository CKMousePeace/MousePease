using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(Investigate))]
[RequireComponent(typeof(Chase))]

public class CBossController : MonoBehaviour
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

    private Animator m_BossANi;
    private NavMeshAgent m_agent;

    public float RemainingDistance { get =>m_agent.remainingDistance; }
    //HoldDown ��ų ����ϸ� ���� �ָ����� �����ٵ� NavMesh ���� �ش� ������ ������ 
    //�� �̴� ���ֵ� ������!!
    public float StoppingDistance { get => m_agent.stoppingDistance; }
    public void SetDestination(Vector3 destination) => m_agent.SetDestination(destination);

    private float m_defaultAgentSpeed;
    public void MultiplySpeed(float factor) => m_agent.speed = m_defaultAgentSpeed * factor;    //�ӵ� ���
    public void SetDefaultSpeed() => m_agent.speed = m_defaultAgentSpeed;

    private BossEyes eyes;
    private BossEars ears;
    public void IgnoreEars(bool ignore) => ears.gameObject.SetActive(!ignore);      //�÷��̾� �߰ݽ� û�� ��Ȱ��ȭ



    private void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_BossANi = GameObject.Find("boss_dummy").GetComponent<Animator>();

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

        if(m_agent.enabled == false)
        {
            throw new System.Exception("HoldDown ��ų �����!");
        }
    }

    public void Patrol()
    {
        CurrentBehavior = m_PatrolBehavior;
    }

    public void Investigate(Detectable detectable)
    {
        m_InvestigateBehavior.Destination = detectable.transform.position;
        CurrentBehavior = m_InvestigateBehavior;
    }

    public void Chase(Detectable detectable)
    {
        m_ChaseBehavior.g_Target = detectable.transform;
        CurrentBehavior = m_ChaseBehavior;
    }


    public void BossAnimation()        //���� Nav���� �ӵ� �޾ƿ��� ���� �ִϸ����Ϳ� �־���
    {
        float velocity = m_agent.velocity.magnitude;
        m_BossANi.SetFloat("Speed", velocity);
    }
}