using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterIdle : CControllerBase
{
    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }

    protected void OnEnable()
    {
        Debug.Log("���� ���� : idle Start");
    }
    protected void OnDisable()
    {
        Debug.Log("���� ���� : idle End");
    }

    private void Update()
    {
        // ����
    }
}
