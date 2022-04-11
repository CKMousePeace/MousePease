using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRock : CStaticObject
{

    [SerializeField] private GameObject DangerZone;

    [Tooltip("Float[0] = Force, Float[1] = Damage")]
    [SerializeField] private CBuffBase.BuffInfo m_Buffinfo;

    private bool isChecker = false;

    protected override void Start()
    {
        base.Start();
        m_Buffinfo.g_Value_Vector3.Add(transform.position);
    }


    private void OnCollisionEnter(Collision col)
    {
        isChecker = true;

        if (col.collider.CompareTag("Player")){

            if (isChecker == true)
            {
                var actor = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CDynamicObject>();
                actor.GenerateBuff("KnockBack", m_Buffinfo);
                DangerZone.SetActive(false);

            }
            else
            {

                DangerZone.SetActive(false);
                
            }
        }
    }

}
