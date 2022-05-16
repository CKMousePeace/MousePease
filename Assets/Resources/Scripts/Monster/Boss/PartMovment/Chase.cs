using UnityEngine;

public class Chase : AIBehaviour
{
    //�÷��̾� �߰�
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
            Debug.Log("�÷��̾� Kill!");
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
            Debug.Log("���� �ƴ϶��!");
        }



    }

    public override void Deactivate(CBossController controller)
    {
        controller.IgnoreEars(false);
    }
}