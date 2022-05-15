using UnityEngine;

public class Patrol : AIBehaviour
{
    //��������Ʈ
    public Transform[] PatrolPoints;
    private int m_currentPPIndex;

    [SerializeField] private GameObject Skill;

    public override void Activate(CBossController controller)
    {
        controller.SetDefaultSpeed();
        controller.SetDestination(PatrolPoints[m_currentPPIndex].position);
    }

    public override void UpdateStep(CBossController controller)
    {
        if (controller.RemainingDistance <= controller.StoppingDistance)
        {
            m_currentPPIndex = m_currentPPIndex < PatrolPoints.Length - 1 ? m_currentPPIndex + 1 : 0;

            controller.SetDestination(PatrolPoints[m_currentPPIndex].position);
        }

        if(m_currentPPIndex == 9 && Skill != null)
        {

            Debug.Log("Ŀ��Ʈ ��� : " + m_currentPPIndex);

            try
            {
                Skill.SetActive(true);
                m_currentPPIndex = m_currentPPIndex + 1;
            }

            catch
            {
                Debug.Log("����");
            }


        }
    }
}