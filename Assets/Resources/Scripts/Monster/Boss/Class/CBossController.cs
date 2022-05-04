using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(Investigate))]
[RequireComponent(typeof(Chase))]

public class CBossController : MonoBehaviour
{
    private AIBehaviour currentBehavior;
    public AIBehaviour CurrentBehavior      //���� ���� 
    {
        get => currentBehavior;
        private set
        {
            currentBehavior?.Deactivate(this);
            value.Activate(this);
            currentBehavior = value;
        }
    }

    private Patrol PatrolBehavior;                  //��Ʈ��(��������Ʈ)
    private Investigate InvestigateBehavior;        //Ÿ�� �ִ��� �ֺ� ����
    private Chase ChaseBehavior;                    //���� �ȿ� ������ �߰�

    private Animator BossANi;
    private NavMeshAgent agent;
    public float RemainingDistance { get => agent.remainingDistance; }
    public float StoppingDistance { get => agent.stoppingDistance; }
    public void SetDestination(Vector3 destination) => agent.SetDestination(destination);

    private float defaultAgentSpeed;
    public void MultiplySpeed(float factor) => agent.speed = defaultAgentSpeed * factor;    //�ӵ� ���
    public void SetDefaultSpeed() => agent.speed = defaultAgentSpeed;

    private BossEyes eyes;
    private BossEars ears;
    public void IgnoreEars(bool ignore) => ears.gameObject.SetActive(!ignore);      //�÷��̾� �߰ݽ� û�� ��Ȱ��ȭ



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        BossANi = GameObject.Find("boss_dummy").GetComponent<Animator>();

        defaultAgentSpeed = agent.speed;
        PatrolBehavior = GetComponent<Patrol>();
        InvestigateBehavior = GetComponent<Investigate>();
        ChaseBehavior = GetComponent<Chase>();

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
    }

    public void Patrol()
    {
        CurrentBehavior = PatrolBehavior;
    }

    public void Investigate(Detectable detectable)
    {
        InvestigateBehavior.Destination = detectable.transform.position;
        CurrentBehavior = InvestigateBehavior;
    }

    public void Chase(Detectable detectable)
    {
        ChaseBehavior.Target = detectable.transform;
        CurrentBehavior = ChaseBehavior;
    }


    public void BossAnimation()        //���� Nav���� �ӵ� �޾ƿ��� ���� �ִϸ����Ϳ� �־���
    {
        float velocity = agent.velocity.magnitude;
        BossANi.SetFloat("Speed", velocity);
    }
}