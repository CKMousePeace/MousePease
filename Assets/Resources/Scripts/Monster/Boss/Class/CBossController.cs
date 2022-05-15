using System.Collections;
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

    public Animator g_BossANi;
    public NavMeshAgent g_agent;
    public GameObject g_Boss;
    public Rigidbody g_RigidBoss;

    public float RemainingDistance { get => g_agent.remainingDistance;  }
    /*HoldDown ��ų ����ϸ� ���� �ָ����� �����ٵ� NavMesh ���� �ش� ������ ������ 
    �� �̴� ���ֵ� ������!!*/

    public float StoppingDistance { get => g_agent.stoppingDistance; }
    public void SetDestination(Vector3 destination) => g_agent.SetDestination(destination);

    private float m_defaultAgentSpeed;
    public void MultiplySpeed(float factor) => g_agent.speed = m_defaultAgentSpeed * factor;    //�ӵ� ���
    public void SetDefaultSpeed() => g_agent.speed = m_defaultAgentSpeed;

    private BossEyes eyes;
    private BossEars ears;
    public void IgnoreEars(bool ignore) => ears.gameObject.SetActive(!ignore);      //�÷��̾� �߰ݽ� û�� ��Ȱ��ȭ

    [SerializeField] private bool m_isCheckIntro = false;

    private void Start()
    {
        if(m_isCheckIntro == true)    StartCoroutine(StartIntro());

        m_defaultAgentSpeed = g_agent.speed;
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

        if (g_agent.enabled == false)
        {
            throw new System.Exception("HoldDown ��ų �����! �����Ƴ�!");
        }
    }

    [SerializeField] private GameObject Smash;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cheese"))
        {
            g_RigidBoss.isKinematic = false;
            Smash.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        g_RigidBoss.isKinematic = true;
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
        float velocity = g_agent.velocity.magnitude;
        g_BossANi.SetFloat("Speed", velocity);
    }

    IEnumerator StartIntro()
    {
        g_agent.speed = 0;
        g_BossANi.SetTrigger("IntroAnimation");

        yield return new WaitForSeconds(12.0f);

        g_agent.speed = 6;

        yield return new WaitForSeconds(5.0f);

        g_agent.velocity = Vector3.zero;
        g_agent.speed = 0;

        yield return new WaitForSeconds(13.0f);

        yield break;


    }

}