using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(Investigate))]
[RequireComponent(typeof(Chase))]
[RequireComponent(typeof(Bite))]

public class CBossController : MonoBehaviour
{
    private AIBehaviour currentBehavior;
    public AIBehaviour CurrentBehavior
    {
        get => currentBehavior;
        private set
        {
            currentBehavior?.Deactivate(this);
            value.Activate(this);
            currentBehavior = value;
        }
    }

    private Patrol PatrolBehavior;
    private Investigate InvestigateBehavior;
    private Chase ChaseBehavior;
    private Bite BiteBehavior;

    public GameObject Boss;                 //애니메이터를 받아올 오브젝트


    private NavMeshAgent agent;
    public float RemainingDistance { get => agent.remainingDistance; }
    public float StoppingDistance { get => agent.stoppingDistance; }
    public void SetDestination(Vector3 destination) => agent.SetDestination(destination);

    private float defaultAgentSpeed;
    public void MultiplySpeed(float factor) => agent.speed = defaultAgentSpeed * factor;
    public void SetDefaultSpeed() => agent.speed = defaultAgentSpeed;

    private BossEyes eyes;
    private BossEars ears;
    public void IgnoreEars(bool ignore) => ears.gameObject.SetActive(!ignore);


    //===============디버그===================//

    [Header("체크할 시 깨물기 스킬 사용")]
    public bool m_DebugBiteMod = false;


    //===============디버그===================//


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        defaultAgentSpeed = agent.speed;

        PatrolBehavior = GetComponent<Patrol>();
        InvestigateBehavior = GetComponent<Investigate>();
        ChaseBehavior = GetComponent<Chase>();
        BiteBehavior = GetComponent<Bite>();

        eyes = GetComponentInChildren<BossEyes>();
        eyes.OnDetect += Chase;
        eyes.OnLost += Investigate;

        ears = GetComponentInChildren<BossEars>();
        ears.OnDetect += Investigate;

        Patrol();
    }

    private void Update()
    {

        if(m_DebugBiteMod == true)
        {
            CurrentBehavior = BiteBehavior;
        }
        else
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
        BossAnimation();
        ChaseBehavior.Target = detectable.transform;
        CurrentBehavior = ChaseBehavior;
    }


    public void Bite(Detectable detectable)
    {

        CurrentBehavior = BiteBehavior;
        
    }


    private void BossAnimation()        //보스 Nav에서 속도 받아오는 값을 애니메이터에 넣어줌
    {
        float velocity = agent.velocity.magnitude;
        Boss.GetComponent<Animator>().SetFloat("Speed", velocity);
    }
}