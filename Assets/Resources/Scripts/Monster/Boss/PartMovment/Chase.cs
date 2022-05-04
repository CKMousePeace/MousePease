using UnityEngine;

public class Chase : AIBehaviour
{
    //�÷��̾� �߰�
    public Transform Target;
    public float AgentSpeedMultiplier = 1f;

    public override void Activate(CBossController controller)
    {
        controller.MultiplySpeed(AgentSpeedMultiplier);
        controller.IgnoreEars(true);
    }

    public override void UpdateStep(CBossController controller)
    {
        controller.SetDestination(Target.position);
    }

    public override void Deactivate(CBossController controller)
    {
        controller.IgnoreEars(false);
    }
}