using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWalk : CControllerBase
{
    [SerializeField] private float m_fSpeed;
    [SerializeField] private float m_currentSpeed = 0;
    [SerializeField] private float m_fRunningSpeed;


    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    private void Awake()
    {

    }


    protected void OnEnable()
    {
        if (m_Actor == null) return;
        m_Actor.g_Animator.SetTrigger("Walk");

        //StartCoroutine(BossWalkStart());

    }
    protected void OnDisable()
    {
        if (m_Actor == null) return;

    }

    private void Update()
    {
        BossWalk();
    }


    private void BossWalk()
    {
        if (m_currentSpeed == 0.0f)
        {
            m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / (m_fRunningSpeed + m_fSpeed)));
            return;
        }
    }
    //private IEnumerator BossWalkStart()
    //{
    //    yield return new WaitUntil(() => {

    //        if (m_Actor.g_Animator.GetCurrentAnimatorStateInfo(0).IsName("BossWalkStart") && m_Actor.g_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
    //        {

    //            m_Actor.g_Animator.SetFloat("Walking", Mathf.Abs(m_currentSpeed / (m_fRunningSpeed + m_fSpeed)));


    //            return true;
    //        }
    //        return false;
    //    });

    //}


}
