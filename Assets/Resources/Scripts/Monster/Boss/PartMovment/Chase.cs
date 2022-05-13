using UnityEngine;

public class Chase : AIBehaviour
{
    //플레이어 추격
    public Transform g_Target;
    public float g_AgentSpeedMultiplier = 1f;
    public GameObject g_KillColiider;

    public override void Activate(CBossController controller)
    {
        g_KillColiider.SetActive(true);
        controller.MultiplySpeed(g_AgentSpeedMultiplier);
        controller.IgnoreEars(true);
    }

    public override void UpdateStep(CBossController controller)
    {
        controller.SetDestination(g_Target.position);
    }

    public override void Deactivate(CBossController controller)
    {
        //g_KillColiider.SetActive(false);
        controller.IgnoreEars(false);
    }
}