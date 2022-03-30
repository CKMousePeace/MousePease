using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;       //For use the nav agent. nav mesh Agent 사용을 위한 선언 

public class CMonMovement : CControllerBase
{
    private int g_currentNode = 0;

    [SerializeField] private NavMeshAgent g_nav;
    [SerializeField] private GameObject g_PlayerTarget;
    [SerializeField] private List<Transform> g_WayPoint = new List<Transform>();
    [SerializeField] private GameObject Walk;


    private void Start()
    {
        g_nav = GameObject.Find("Boss").GetComponent<NavMeshAgent>();
        g_PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        NextIndex();
    }

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }


    private void Update()
    {
        if (g_PlayerTarget.activeSelf == true)
        {
            //BossMovement();
            BossWayPointer();
        }
        else
            return;

    }

    private void BossMovement()
    {

        if(g_nav.destination != g_PlayerTarget.transform.position)
        {
            Walk.gameObject.SetActive(true);
            g_nav.SetDestination(g_PlayerTarget.transform.position);         
        }

        else
        {
            Walk.gameObject.SetActive(false);
            g_nav.SetDestination(transform.position);
        }

    }

    private void BossWayPointer()
    {
        
        if (!g_nav.pathPending)
        { //&& g_nav.remainingDistance < 1.0f
            Walk.gameObject.SetActive(true);
            NextIndex();
        }
        //Execute if distance to destination is less than 1 or arrives 목적지까지의 거리가 1 이하 혹은 도착했으면 실행 

        if (g_currentNode == g_WayPoint.Count)
        {
            Debug.Log("현재 노드 : " + g_currentNode);
            Debug.Log("웨이포인트 : " + g_WayPoint);
            Debug.Log("웨이포인트 카운트 : " + g_WayPoint.Count);
            g_currentNode = 0;
        }

        //Arrive at the last note? -> Initialize.  마지막 노트에 도착? -> 초기화.
    }




    private void NextIndex()
    {
        g_nav.destination = g_WayPoint[g_currentNode].position;
        g_currentNode = (g_currentNode + 1);
        //moving position 위치 이동

    }

    private void OnDrawGizmos()     //Draw to visually express the position 그려서 위치 시각적으로 표현
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
