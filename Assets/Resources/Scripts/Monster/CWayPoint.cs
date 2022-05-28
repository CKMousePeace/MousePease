using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWayPoint : CControllerBase
{


    public float g_speed = 5f;
    private Transform m_Target;
    private int m_WayIndex = 0;
    [SerializeField] private GameObject m_Walk;

    public override void init(CDynamicObject actor)
    {
       //gameObject.SetActive(true);
        base.init(actor);
    }


    private void Start()
    {
        //m_Walk = GameObject.Find("Walk");
        //m_Target = CWayPointer.g_Points[0];
    }


    protected void OnEnable()
    {
        //m_Walk.SetActive(true);

    }
    protected void OnDisable()
    {
        //m_Actor.g_Animator.SetTrigger("Idle");
        //m_Walk.SetActive(false);
    }

    private void Update()
    {

        //Vector3 dir = m_Target.position - m_Actor.transform.position;
        ////waypoint ~ Boss distance  ��������Ʈ ~ �ڽ� �Ÿ�  

        //m_Actor.transform.Translate(dir.normalized * g_speed * Time.deltaTime, Space.World);

        //if (Vector3.Distance(m_Actor.transform.position, m_Target.position) <= 0.4f)
        //{
        //    //���� �ε��������� �Ÿ��� 0.4 ���� ������ ���� �ε�����  If the distance to the next index is less than 0.4, it moves to the next index.
        //    NextIndex();
        //}


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
