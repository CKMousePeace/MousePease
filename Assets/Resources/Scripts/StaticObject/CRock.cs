using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRock : CStaticObject
{

    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    [SerializeField] GameObject Root;
    [SerializeField] private float DestroyWaitTime = 0;
    [SerializeField] private float CrashWaitTime = 0;

    private bool isChecker = false;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);
        StartCoroutine(SelfDestroy(DestroyWaitTime));
    }


    private void OnCollisionEnter(Collision col)
    {
        isChecker = true;

        if (col.collider.CompareTag("Player")){

            if (isChecker == true)
            {
                var actor = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CDynamicObject>();
                actor.GenerateBuff("KnockBack", m_Buffinfo);

                StartCoroutine(SelfDestroy(CrashWaitTime));
            }

        }
    }

    IEnumerator SelfDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(Root);
    }

}
