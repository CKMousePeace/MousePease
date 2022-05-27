using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CMonMovement : CControllerBase
{
    [SerializeField] private GameObject m_Camrea;
    [SerializeField] private NavMeshAgent m_nav;
    [SerializeField] private GameObject m_PlayerTarget;     //Target (Player)
    [SerializeField] private List<Transform> m_WayPoint = new List<Transform>();


    private int m_currentNode = 0;

    //===============�����===================//

    [Header("false = �Ϲ� Ʈ��ŷ / true = ��������Ʈ")]
    [SerializeField] private bool m_DebugTrackingMod = false;

    [Header("üũ�� �� ���� ������")]
    [SerializeField] private bool m_DebugMoveMod = false;

    [Header("üũ�� �� ������ ��ų ���")]
    [SerializeField] private bool m_DebugBiteMod = false;

    [Header("üũ�� �� ��ġ�� ��ų ���")]     //Hold Down
    [SerializeField] private bool m_DebugHoldDMod = false;

    //===============�����===================//


    private void Start()
    {      
        m_nav = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
        m_nav.autoBraking = false;
        m_nav.isStopped = false;
        m_PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        NextIndex();
    }

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }

    private void BossAnimation()        //���� Nav���� �ӵ� �޾ƿ��� ���� �ִϸ����Ϳ� �־���
    {
        float velocity = m_nav.velocity.magnitude;
        m_Actor.g_Animator.SetFloat("Speed", velocity);
    }



    [Header("������ ���� �ð�")]
    [SerializeField] private float m_MonBiteRunningTime = 2.0f; //Bite Collider duration

    [Header("��ġ�� ��� �ð�")]
    [SerializeField] private float m_MonHoldDWaitTime = 2.0f; //Bite Collider duration

    [Header("��ģ �� ���� �� �ð�")]
    [SerializeField] private float m_MonHoldDAfterTime = 2.0f; //Bite Collider duration

    private void Update()
    {
        //������ üũ
        if (m_DebugMoveMod == true)
        {
            if (m_DebugTrackingMod == false)
            {
                if (m_PlayerTarget.activeSelf == true)  BossMovement();
                else
                {
                    m_nav.autoBraking = true;
                    m_nav.velocity = Vector3.zero;
                    m_currentNode = 0;
                    return;
                }
            }
            else if (m_DebugTrackingMod == true)
            {
                if (m_PlayerTarget.activeSelf == true)
                {
                    BossAnimation();
                    BossWayPointer();

                }
                else
                {
                    m_nav.autoBraking = true;
                    m_nav.velocity = Vector3.zero;
                    m_currentNode = 0;
                    return;
                }
            }
        }

        //������ ����
        if(m_DebugBiteMod == true)  StartCoroutine(BiteMode(m_MonBiteRunningTime));

        //��ġ�� ����
        if (m_DebugHoldDMod == true) StartCoroutine(HoldDownMode(m_MonHoldDWaitTime, m_MonHoldDAfterTime));
    }

    private void BossMovement()
    {
        if(m_nav.destination != m_PlayerTarget.transform.position)
        {
            
            m_nav.SetDestination(m_PlayerTarget.transform.position);
            BossAnimation();
        }
        else
        {
            m_nav.SetDestination(transform.position);
        }
    }

    private void BossWayPointer()
    {
        //Execute if distance to destination is less than 5 or arrives ������������ �Ÿ��� 3 ���� Ȥ�� ���������� ���� 
        if (!m_nav.pathPending && m_nav.remainingDistance < 3.0f)
        {
            NextIndex();
        }
    }

    private void NextIndex()
    {
        if (m_currentNode == m_WayPoint.Count)
        {
            m_nav.speed = 0;
            return;
        }
        else
        {       
            m_nav.destination = m_WayPoint[m_currentNode].position;
            m_currentNode = (m_currentNode + 1);
            //moving position ��ġ �̵�
            //Debug.Log("���� ��� : " + m_currentNode);
        }


        switch (m_currentNode)
        {
            case 4:
                //StartCoroutine("JumpDelay");
                break;
        }
    }



    [SerializeField] private GameObject m_BiteCollider;     //BiteCollider Add
    IEnumerator BiteMode(float Time)    //������ ����
    {
        while (true)
        {
            m_BiteCollider.SetActive(true);
            m_nav.speed = 10.0f;

            yield return new WaitForSeconds(Time);

            m_nav.speed = 6.0f;
            m_BiteCollider.SetActive(false);

            m_DebugBiteMod = false;

            yield break;
        }
    }

    [SerializeField] private GameObject m_HoldDownCollider;
    IEnumerator HoldDownMode(float Ready , float Loop)    //��ġ�� ����
    {

        while (true)
        {
            m_nav.isStopped = true;
            m_Actor.g_Animator.SetTrigger("HoldReady");

            yield return new WaitForSeconds(Ready);

            m_Actor.g_Animator.SetTrigger("HoldLoop");  //���� ���� �־����
            m_HoldDownCollider.SetActive(true);
            m_nav.isStopped = false;
            yield return new WaitForSeconds(Loop);

            m_Actor.g_Animator.SetTrigger("HoldFinish");
            yield return new WaitForSeconds(2.0f);

            m_DebugHoldDMod = false;
            yield break;
        }


    }

    private void OnDrawGizmos()     //Draw to visually express the position �׷��� ��ġ �ð������� ǥ��
    {
        for (int i = 0; i < m_WayPoint.Count; i++)
        {
            Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            Gizmos.DrawSphere(m_WayPoint[i].transform.position, 2);
            Gizmos.DrawWireSphere(m_WayPoint[i].transform.position, 2f);

            if (i < m_WayPoint.Count - 1)
            {
                if (m_WayPoint[i] && m_WayPoint[i + 1])
                {
                    Gizmos.color = Color.blue;
                    if (i < m_WayPoint.Count - 1)
                        Gizmos.DrawLine(m_WayPoint[i].position, m_WayPoint[i + 1].position);
                    if (i < m_WayPoint.Count - 2)
                    {
                        Gizmos.DrawLine(m_WayPoint[m_WayPoint.Count - 1].position, m_WayPoint[0].position);
                    }
                }
            }
        }
    }
}