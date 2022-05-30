using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterIdle : CControllerBase
{
    public override void init(CDynamicObject actor)
    {
        //우선 비활성화 시켜두고 가만히 있을때 ( 인 게임에선 가만히 있을 일은 없을 듯) 따로 활성화
        gameObject.SetActive(false);
        base.init(actor);
    }

    protected void OnEnable()
    {
        m_Actor.g_Animator.SetTrigger("Idle");
    }
    protected void OnDisable()
    {
    }

    private void Update()
    {
        
        // 읽힘
    }
}
