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
        Debug.Log("몬스터 상태 : idle Start");
    }
    protected void OnDisable()
    {
        Debug.Log("몬스터 상태 : idle End");
    }

    private void Update()
    {
        // 읽힘
    }
}
