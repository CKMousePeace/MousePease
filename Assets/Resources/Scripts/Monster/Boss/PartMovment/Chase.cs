using UnityEngine;

public class Chase : AIBehaviour
{
    //플레이어 추격
    public Transform g_Target;
    public float g_AgentSpeedMultiplier = 1f;
    [SerializeField] private GameObject m_KillColiider;

    public override void Activate(CBossController controller)
    {
        
        try
        {
            m_KillColiider.SetActive(true);
        }

        catch
        {
            Debug.Log("플레이어 Kill!");
            return;
        }

        controller.MultiplySpeed(g_AgentSpeedMultiplier);
        controller.IgnoreEars(true);
    }

    public override void UpdateStep(CBossController controller)
    {
        try
        {
            controller.SetDestination(g_Target.position);
        }
        catch
        {
            Debug.Log("에러 아니라고!");
        }
    }

    public override void Deactivate(CBossController controller)
    {
        controller.IgnoreEars(false);
    }
}