using UnityEngine;

public class Bite : AIBehaviour
{
    public GameObject BiteCollider;

    public override void Activate(CBossController controller)
    {
        BiteCollider.SetActive(true);
        controller.m_DebugBiteMod = false;
    }

    public override void UpdateStep(CBossController aIController)
    {
        throw new System.NotImplementedException();
    }
}
