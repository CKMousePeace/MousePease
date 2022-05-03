using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRock : CStaticObject
{

    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    [SerializeField] GameObject m_Root;
    [SerializeField] private float m_DestroyWaitTime = 0;
    [SerializeField] private float m_CrashWaitTime = 0;

    private bool isChecker = false;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);
        StartCoroutine(SelfDestroy(m_DestroyWaitTime));
    }


    private void OnCollisionEnter(Collision col)
    {
        isChecker = true;

        if (col.collider.CompareTag("Player")){

            if (isChecker == true)
            {
                var actor = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CDynamicObject>();
                actor.GenerateBuff("KnockBack", m_Buffinfo);

                StartCoroutine(SelfDestroy(m_CrashWaitTime));
            }

        }
    }

    IEnumerator SelfDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        //DestroyImmediate(m_Root);
        gameObject.SetActive(false);
    }

}
