using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWayPoint : CControllerBase
{
    public float g_speed = 5f;
    private Transform g_Target;
    private int g_WayIndex = 0;


    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(true);
        base.init(actor);
    }


    private void Start()
    {
        g_Target = CWayPointer.g_Points[0];
    }


    protected void OnEnable()
    {
    }
    protected void OnDisable()
    {
    }

    private void Update()
    {

        Vector3 dir = g_Target.position - m_Actor.transform.position;
        //waypoint ~ Boss distance  ��������Ʈ ~ �ڽ� �Ÿ�  

        m_Actor.transform.Translate(dir.normalized * g_speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(m_Actor.transform.position, g_Target.position) <= 0.4f)
        {
            //���� �ε��������� �Ÿ��� 0.4 ���� ������ ���� �ε�����  If the distance to the next index is less than 0.4, it moves to the next index.
            NextIndex();
        }


    }

    private void NextIndex()        //move to next index  ���� �ε����� �̵�
    {
        if (g_WayIndex >= CWayPointer.g_Points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        g_WayIndex++;
        g_Target = CWayPointer.g_Points[g_WayIndex];

    }





}
