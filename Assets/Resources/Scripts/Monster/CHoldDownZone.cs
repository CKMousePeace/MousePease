using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CHoldDownZone : CStaticObject
{

    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    [SerializeField] private CDynamicObject m_DynamicObject;
    private bool isChecker = false;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);
    }


    private void OnTriggerEnter(Collider col)
    {
        isChecker = true;

        if (col.CompareTag("Player") && isChecker == true)
        {
            StartCoroutine(AttackDelay());
        }     
    }

    private void OnTriggerExit(Collider col)
    {
        isChecker = false;
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitUntil(() => {
            bool Check = (m_DynamicObject.g_Animator.GetCurrentAnimatorStateInfo(0).IsName("BossThrow") &&
            m_DynamicObject.g_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f);
            return Check;
        });

        gameObject.SetActive(false);
    }


}
