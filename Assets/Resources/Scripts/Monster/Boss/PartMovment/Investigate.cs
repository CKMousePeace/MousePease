using UnityEngine;

public class Investigate : AIBehaviour
{
    //플레이어 주변에 있는지 확인
    public Vector3 Destination;
    public int InvestigateForSeconds = 3;
    public float AgentSpeedMultiplier = 1.5f;

    private bool m_isInvestigating;
    private float m_investigationStartTime;

    public override void Activate(CBossController controller)
    {
        controller.MultiplySpeed(AgentSpeedMultiplier);
        controller.SetDestination(Destination);
    }

    public override void UpdateStep(CBossController controller)
    {
        if (controller.RemainingDistance <= controller.StoppingDistance && !m_isInvestigating)
        {
            m_isInvestigating = true;
            m_investigationStartTime = Time.time;
        }

        if (m_isInvestigating && Time.time > m_investigationStartTime + InvestigateForSeconds)
        {
            m_isInvestigating = false;
            controller.Patrol();
        }
    }

    public override void Deactivate(CBossController aIController)
    {
        m_isInvestigating = false;
    }
}