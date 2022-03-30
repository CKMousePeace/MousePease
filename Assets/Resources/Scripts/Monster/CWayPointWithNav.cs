using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWayPointWithNav : CControllerBase
{
    private int g_currentNode = 0;
    [SerializeField] private UnityEngine.AI.NavMeshAgent g_Agent;
    [SerializeField] private List<Transform> g_WayPoint = new List<Transform>();
    [SerializeField] private GameObject Walk;
    [SerializeField] private GameObject Run;


    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }


    private void Start()
    {
        g_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        NextIndex();
    }


    private void Update()
    {
        if (g_Agent.remainingDistance < 5.0f)
        {
            Walk.gameObject.SetActive(true);
            NextIndex();
        }
        //Execute if distance to destination is less than 1 or arrives ������������ �Ÿ��� 1 ���� Ȥ�� ���������� ���� 


        if (g_currentNode == g_WayPoint.Count)
        {
            g_currentNode = 0;
            Walk.gameObject.SetActive(false);
        }
        
        //Arrive at the last note? -> Initialize.  ������ ��Ʈ�� ����? -> �ʱ�ȭ.
    }

    private void NextIndex()
    {
        g_Agent.destination = g_WayPoint[g_currentNode].position;
        g_currentNode = (g_currentNode + 1);
        //moving position ��ġ �̵�

    }

    private void OnDrawGizmos()     //Draw to visually express the position �׷��� ��ġ �ð������� ǥ��
    {
        for(int i =0; i < g_WayPoint.Count; i++)
        {
            Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
            Gizmos.DrawSphere(g_WayPoint[i].transform.position, 2);
            Gizmos.DrawWireSphere(g_WayPoint[i].transform.position, 20f);

            if( i < g_WayPoint.Count -1) {
                if(g_WayPoint[i] && g_WayPoint[ i + 1])
                {
                    Gizmos.color = Color.blue;
                    if(i < g_WayPoint.Count -1)
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
