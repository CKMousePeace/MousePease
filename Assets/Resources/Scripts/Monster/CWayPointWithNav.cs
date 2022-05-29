using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWayPointWithNav : CControllerBase
{
    private int g_currentNode = 0;
    private UnityEngine.AI.NavMeshAgent g_Agent;
    private List<Transform> g_WayPoint = new List<Transform>();


    public float g_speed = 5f;
    private Transform m_Target;
    private int m_WayIndex = 0;

    public override void init(CDynamicObject actor)
    {
       //gameObject.SetActive(true);
        base.init(actor);
    }


    private void Start()
    {
        //m_Target = CWayPointer.g_Points[0];
    }


    protected void OnEnable()
    {

    }
    protected void OnDisable()
    {

    }

    private void Update()
    {

   

    }

    private void NextIndex()        //move to next index  ���� �ε����� �̵�
    {
        if (m_WayIndex >= CWayPointer.g_Points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        if (m_WayIndex == 2)        
            //In the case of the 3nd index, adjust and speed up with Anime Run.  3��° �ε����ϰ�� �ִϸ� Run ���� ���� �� �ӵ� ����
        {
            m_Actor.g_Animator.SetTrigger("Run");
            g_speed = 4;

        }

        m_WayIndex++;
        m_Target = CWayPointer.g_Points[m_WayIndex];

    }





}
