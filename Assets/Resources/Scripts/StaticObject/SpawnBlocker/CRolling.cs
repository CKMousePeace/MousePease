using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRolling : CStaticObject
{
    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;
    [SerializeField] private CBuffBase.BuffInfo m_BuffinfoNoDamage;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);
    }

    private void OnCollisionEnter(Collision col)
    {
     
        if (col.gameObject.CompareTag("Player"))
        {
            var actor = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CDynamicObject>();
            if (!CrushChecker(actor))
            {
                actor.GenerateBuff("KnockBack", m_Buffinfo);
                actor.GenerateBuff("Invincibility", m_BuffinfoNoDamage);

            }

            gameObject.SetActive(false);
        }

        if (col.gameObject.CompareTag("Boss"))
            gameObject.SetActive(false);

    }


    private void FixedUpdate()
    {
        if (gameObject.transform.position.z < -15)
            gameObject.SetActive(false);

    }

}
