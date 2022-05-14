using UnityEngine;
using UnityEngine.AI;

public class CSmash: CControllerBase
{
    
    public override void init(CDynamicObject actor)
    {
        gameObject.SetActive(false);
        base.init(actor);
    }

    [SerializeField] private NavMeshAgent m_nav;
    [SerializeField] private GameObject HoldDown;
    protected void OnEnable()
    {
        if (m_Actor == null) return;

        if (HoldDown.activeSelf == false)
        {
            m_nav.velocity = Vector3.zero;
            m_Actor.g_Animator.SetTrigger("Throw");
        }
        else return;

        if (m_Actor.CompareController("MonMovement") || m_Actor.CompareController("MonBite"))
        {
            gameObject.SetActive(false);
            return;
        }

    }
    protected void OnDisable()
    {
        if (m_Actor == null) return;
    }

    private void Update()
    {

    }
}
