using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;       //For use the nav agent. nav mesh Agent ����� ���� ���� 

public class CMonMovement : CControllerBase
{
    [SerializeField] private NavMeshAgent g_nav;
    [SerializeField] private GameObject g_PlayerTarget;
    [SerializeField] private List<Transform> g_WayPoint = new List<Transform>();
    [SerializeField] private float m_fSpeed = 4;
    [SerializeField] private float m_fRunningSpeed = 2;
    private bool g_isMove = true;
    private int g_currentNode = 0;



    //===============�����===================//

    [Header("false = �Ϲ� Ʈ��ŷ / true = ��������Ʈ")]
    [SerializeField] private bool m_DebugTrackingMod = false;

    [Header("üũ�� �� ���� ������")]
    [SerializeField] private bool m_DebugMoveMod = false;


    //===============�����===================//


    private void Start()
    {
        g_nav = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
        g_nav.autoBraking = false;
        g_PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        NextIndex();
    }

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }


    private void FixedUpdate()
    {

        if (m_DebugMoveMod == true)
        {
            if (m_DebugTrackingMod == false)
            {

                if (g_PlayerTarget.activeSelf == true)
                {
                    BossMovement();
                }
                else
                {
                    g_isMove = false;
                    g_nav.speed = 0;
                    g_currentNode = 0;
                    m_fSpeed = 0;
                    m_fRunningSpeed = 0;
                    m_Actor.g_Animator.SetBool("isMove", false);
                    return;

                }
            }
            else if (m_DebugTrackingMod == true)
            {
                //Debug.Log("isMove : " + g_isMove);
                if (g_PlayerTarget.activeSelf == true)
                {
                    BossWayPointer();

                }
                else
                {
                    g_isMove = false;
                    g_nav.speed = 0;
                    g_currentNode = 0;
                    m_fSpeed = 0;
                    m_fRunningSpeed = 0;
                    m_Actor.g_Animator.SetBool("isMove", false);
                    return;

                }
            }
        }
        else
            return;



    }

    private void BossMovement()
    {

        if(g_nav.destination != g_PlayerTarget.transform.position)
        {
            g_nav.autoBraking = true;
            g_isMove = true;

            g_nav.SetDestination(g_PlayerTarget.transform.position);

            
            if (g_isMove == true)
            {
                m_Actor.g_Animator.SetFloat("Speed", Mathf.Abs(g_nav.speed / (m_fRunningSpeed + m_fSpeed)));
            }
        }

        //else if ()

        else
        {
            g_nav.SetDestination(transform.position);
        }

    }

    private void BossWayPointer()
    {
        // && g_nav.remainingDistance < 2.0f
        if (!g_nav.pathPending && g_nav.remainingDistance < 2.0f)
        {
            g_isMove = true;
            if (g_isMove == true)
            {
                m_Actor.g_Animator.SetFloat("Speed", Mathf.Abs(g_nav.speed / (m_fRunningSpeed + m_fSpeed)));

            }
            NextIndex();
    
        }
        //Execute if distance to destination is less than 2 or arrives ������������ �Ÿ��� 2 ���� Ȥ�� ���������� ���� 

        if (g_currentNode == g_WayPoint.Count)
        {
            g_currentNode = 0;
        }

        //Arrive at the last note? -> Initialize.  ������ ��Ʈ�� ����? -> �ʱ�ȭ.
    }

    private void NextIndex()
    {
        g_nav.autoBraking = false;


        g_nav.destination = g_WayPoint[g_currentNode].position;
        g_currentNode = (g_currentNode + 1);
        //moving position ��ġ �̵�


    }

    private void OnDrawGizmos()     //Draw to visually express the position �׷��� ��ġ �ð������� ǥ��
    {
        for (int i = 0; i < g_WayPoint.Count; i++)
        {
            Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            Gizmos.DrawSphere(g_WayPoint[i].transform.position, 2);
            Gizmos.DrawWireSphere(g_WayPoint[i].transform.position, 2f);

            if (i < g_WayPoint.Count - 1)
            {
                if (g_WayPoint[i] && g_WayPoint[i + 1])
                {
                    Gizmos.color = Color.blue;
                    if (i < g_WayPoint.Count - 1)
                        Gizmos.DrawLine(g_WayPoint[i].position, g_WayPoint[i + 1].position);
                    if (i < g_WayPoint.Count - 2)
                    {
                        Gizmos.DrawLine(g_WayPoint[g_WayPoint.Count - 1].position, g_WayPoint[0].position);
                    }
                }
            }
        }
    }
}
