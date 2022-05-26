using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CDeadZone : CBossController
{
    [SerializeField] private CDynamicObject m_DynamicObject;

    public override void init(CDynamicObject actor)
    {
        base.init(actor);
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            g_agent.velocity = Vector3.zero;
            m_Actor.g_Animator.SetTrigger("AttackReady01");                      
            StartCoroutine(AttackDelay());

        }

    }

    IEnumerator AttackDelay()
    {
        yield return new WaitUntil(() => {
            bool Check = (m_DynamicObject.g_Animator.GetCurrentAnimatorStateInfo(0).IsName("AttackReady01") &&
            m_DynamicObject.g_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f);

            GameObject.FindGameObjectWithTag("Player").GetComponent<CPlayer>().g_IsDead = true;
            Destroy(gameObject);

            return Check;
        });

    }
    
}
