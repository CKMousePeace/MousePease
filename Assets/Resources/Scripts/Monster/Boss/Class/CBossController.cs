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

    public Animator g_BossANi;
    public NavMeshAgent g_agent;
    public GameObject g_Boss;
    public Rigidbody g_RigidBoss;

    public float RemainingDistance { get => g_agent.remainingDistance;  }
    /*HoldDown 스킬 사용하면 오류 주르르륵 나올텐데 NavMesh 꺼서 해당 오류가 나오는 
    것 이니 놔둬도 괜찮음!!*/

    public float StoppingDistance { get => g_agent.stoppingDistance; }
    public void SetDestination(Vector3 destination) => g_agent.SetDestination(destination);

    private float m_defaultAgentSpeed;
    public void MultiplySpeed(float factor) => g_agent.speed = m_defaultAgentSpeed * factor;    //속도 배수
    public void SetDefaultSpeed() => g_agent.speed = m_defaultAgentSpeed;

    private BossEyes eyes;
    private BossEars ears;
    public void IgnoreEars(bool ignore) => ears.gameObject.SetActive(!ignore);      //플레이어 추격시 청각 비활성화

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
            throw new System.Exception("HoldDown 스킬 사용중! 오류아냐!");
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