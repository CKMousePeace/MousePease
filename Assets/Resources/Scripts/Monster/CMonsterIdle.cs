using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterIdle : CControllerBase
{
    public override void init(CDynamicObject actor)
    {
        //�켱 ��Ȱ��ȭ ���ѵΰ� ������ ������ ( �� ���ӿ��� ������ ���� ���� ���� ��) ���� Ȱ��ȭ
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
        
        // ����
    }
}
