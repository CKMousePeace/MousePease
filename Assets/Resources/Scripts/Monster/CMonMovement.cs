using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CMonMovement : CControllerBase
{
    [SerializeField] private NavMeshAgent m_nav;
    [SerializeField] private GameObject m_PlayerTarget;
    [SerializeField] private List<Transform> m_WayPoint = new List<Transform>();
    private int m_currentNode = 0;

    //===============디버그===================//

    [Header("false = 일반 트래킹 / true = 웨이포인트")]
    [SerializeField] private bool m_DebugTrackingMod = false;

    [Header("체크할 시 보스 움직임")]
    [SerializeField] private bool m_DebugMoveMod = false;


    //===============디버그===================//


    private void Start()
    {
        m_nav = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
        m_nav.autoBraking = false;
        m_PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        NextIndex();
    }

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }

    private void BossAnimation()        //보스 Nav에서 속도 받아오는 값을 애니메이터에 넣어줌
    {
        float velocity = m_nav.velocity.magnitude;
        m_Actor.g_Animator.SetFloat("Speed", velocity);
    }

    private void Update()
    {
        if (m_DebugMoveMod == true)
        {
            if (m_DebugTrackingMod == false)
            {
                if (m_PlayerTarget.activeSelf == true)
                {
                    BossMovement();
                }
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
        else
            return;
    }

    private void BossMovement()
    {
        if(m_nav.destination != m_PlayerTarget.transform.position)
        {
            m_nav.autoBraking = true;
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
        //Execute if distance to destination is less than 5 or arrives 목적지까지의 거리가 3 이하 혹은 도착했으면 실행 
        if (!m_nav.pathPending && m_nav.remainingDistance < 3.0f)
        {
            //BossAnimation();
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
            m_nav.autoBraking = false;
            m_nav.destination = m_WayPoint[m_currentNode].position;
            m_currentNode = (m_currentNode + 1);
            //moving position 위치 이동
            Debug.Log("현재 노드 : " + m_currentNode);
        }


        switch (m_currentNode)
        {
            case 0:
                break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                m_Actor.g_Animator.SetTrigger("Jump");
                break;
        }


    }

    private void OnDrawGizmos()     //Draw to visually express the position 그려서 위치 시각적으로 표현
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
