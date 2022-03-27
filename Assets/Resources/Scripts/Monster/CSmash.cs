using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSmash: CControllerBase
{
    [SerializeField] private float m_Speed = 0;

    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
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
        //m_Actor.g_Animator.SetBool("Mon_Smash", true);
        // ����
    }
}
