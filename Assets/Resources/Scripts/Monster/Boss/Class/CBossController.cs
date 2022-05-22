using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class CBossController : CControllerBase
{
    private AIBehaviour m_currentBehavior;
    public AIBehaviour CurrentBehavior      //현재 상태 
    {
        get => m_currentBehavior;
        private set
        {
            m_currentBehavior?.Deactivate(this);
            value.Activate(this);
            m_currentBehavior = value;
        }
    }

    private Patrol m_PatrolBehavior;                  //패트롤(웨이포인트)
    private Investigate m_InvestigateBehavior;        //타겟 있는지 주변 조사
    private Chase m_ChaseBehavior;                    //센서 안에 들어오면 추격

    //==네비메싀==//
    [SerializeField]
    private NavMeshAgent m_agent;
    public NavMeshAgent g_agent => m_agent;

    //==리쥐드바뒤==//
    [SerializeField]
    private Rigidbody m_RigidBoss;
    public Rigidbody g_RigidBoss => m_RigidBoss;

    public float RemainingDistance { get => m_agent.remainingDistance;  }
    /*HoldDown 스킬 사용하면 오류 주르르륵 나올텐데 NavMesh 꺼서 해당 오류가 나오는 
    것 이니 놔둬도 괜찮음!!*/

    public float StoppingDistance { get => m_agent.stoppingDistance; }
    public void SetDestination(Vector3 destination) => m_agent.SetDestination(destination);

    private float m_defaultAgentSpeed;
    public void MultiplySpeed(float factor) => m_agent.speed = m_defaultAgentSpeed * factor;    //속도 배수
    public void SetDefaultSpeed() => m_agent.speed = m_defaultAgentSpeed;

    private BossEyes eyes;
    private BossEars ears;
    public void IgnoreEars(bool ignore) => ears.gameObject.SetActive(!ignore);      //플레이어 추격시 청각 비활성화


    [Header("인트로씬 활성화 여부")]
    public bool g_isCheckIntro = false;         //인트로씬 활성화 여부



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
            throw new System.Exception("HoldDown 스킬 사용중! 오류아냐!");
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


    public void Patrol()        //웨이포인트
    {
        CurrentBehavior = m_PatrolBehavior;
    }

    public void Investigate(Detectable detectable)      //주변 탐색
    {
        m_InvestigateBehavior.Destination = detectable.transform.position;
        CurrentBehavior = m_InvestigateBehavior;
    }

    public void Chase(Detectable detectable)        //센서에 플레이어 탐지. 추격
    {
        m_ChaseBehavior.g_Target = detectable.transform;
        CurrentBehavior = m_ChaseBehavior;
    }

    public void BossAnimation()        //보스 Nav에서 속도 받아오는 값을 애니메이터에 넣어줌
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