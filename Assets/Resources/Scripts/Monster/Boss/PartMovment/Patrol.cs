using UnityEngine;

public class Patrol : AIBehaviour
{
    //웨이포인트
    public Transform[] PatrolPoints;
    private int m_currentPPIndex;

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
    }
}