using UnityEngine;

public class Investigate : AIBehaviour
{
    //플레이어 주변에 있는지 확인
    public Vector3 Destination;
    public int InvestigateForSeconds = 3;
    public float AgentSpeedMultiplier = 1.5f;

    private bool isInvestigating;
    private float investigationStartTime;

    public override void Activate(CBossController controller)
    {
        controller.MultiplySpeed(AgentSpeedMultiplier);
        controller.SetDestination(Destination);
    }

    public override void UpdateStep(CBossController controller)
    {
        if (controller.RemainingDistance <= controller.StoppingDistance && !isInvestigating)
        {
            isInvestigating = true;
            investigationStartTime = Time.time;
        }

        if (isInvestigating && Time.time > investigationStartTime + InvestigateForSeconds)
        {
            isInvestigating = false;
            controller.Patrol();
        }
    }

    public override void Deactivate(CBossController aIController)
    {
        isInvestigating = false;
    }
}