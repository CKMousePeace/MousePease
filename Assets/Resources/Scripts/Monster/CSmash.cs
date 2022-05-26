using System.Collections;
using UnityEngine;

public class CSmash: CBossController
{
    
    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    protected void OnEnable()
    {
        if (m_Actor == null) return;

        StartCoroutine(DelaySmash(2));



        if (m_Actor.CompareController("MonBite"))
        {
            gameObject.SetActive(false);
            return;
        }

    }
    protected void OnDisable()
    {
        if (m_Actor == null) return;
    }


    private void FixedUpdate()
    {
        
    }


    IEnumerator DelaySmash(int Count)
    {
        g_agent.velocity = Vector3.zero;
        g_agent.speed = 0;
        m_Actor.g_Animator.SetTrigger("Throw");
        yield return new WaitForSeconds(Count);

        g_agent.speed = 6;

        gameObject.SetActive(false);
    }
    
}
