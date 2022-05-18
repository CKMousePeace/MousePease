using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CMonJump : CBossController
{
    private bool m_IsStart = false;
    private bool m_isGround = false;
    private bool RayCastBoii = false;


    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }


    private void Update()
    {
        if(m_IsStart == true)
        {
            Debug.DrawRay(m_Actor.transform.position, m_Actor.transform.up * -1, Color.blue, 0.3f);

            if (RayCastBoii == true)
            {
                BossJumpDetection();
            }
            else return;
        }

        if (m_isGround == true) gameObject.SetActive(false);
    }

    protected void OnEnable()
    {
        if (m_Actor == null) return;

        m_IsStart = true;
        StartCoroutine(BossJump());

        g_agent.velocity = Vector3.zero;
        m_Actor.g_Animator.SetTrigger("Jump");

    }

    protected void OnDisable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Animator.SetBool("isGround", true);

        m_IsStart = false;
    }



    private void BossJumpDetection()
    {
        RaycastHit hit;
        float MaxDistance = 8.0f;
        int layerMask = 1 << LayerMask.NameToLayer("Floor");
        if (Physics.Raycast(m_Actor.transform.position, m_Actor.transform.up * -1, out hit, MaxDistance, layerMask))
        {
            Debug.Log("Á¡ÇÁ ¶¥¿¡ ´ê¾Ò¾î!");
            m_Actor.g_Animator.SetBool("isGround", true);
            m_isGround = true;
        }
    }

    IEnumerator BossJump()
    {
        yield return new WaitForSeconds(1.0f);
        RayCastBoii = true;
        m_Actor.g_Animator.SetBool("isGround", false);
    }


}
