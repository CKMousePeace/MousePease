using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    public virtual void Activate(CBossController aIController) { }
    public abstract void UpdateStep(CBossController aIController);
    public virtual void Deactivate(CBossController aIController) { }
}