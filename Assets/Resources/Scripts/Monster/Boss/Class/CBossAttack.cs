using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class CBossAttack : MonoBehaviour
{
    public Attackable Attackable;
    protected bool IsAttacking;
    public Animator BossAni;
    public NavMeshAgent BossNav;


    public UnityAction<Attackable> OffAttack;
    public UnityAction<Attackable> OnBiteAttack;
    public UnityAction<Attackable> OnHoldDownAttack;

    
    private void Start()
    {
        
    }


    private void NoAttack(Attackable attackable)        //0
    {
        IsAttacking = false;
        OffAttack?.Invoke(attackable);
    }

    private void BiteAttack(Attackable attackable)      //1
    {
        IsAttacking = true;
        OnBiteAttack?.Invoke(attackable);
    }

    private void HoldDownAttack(Attackable attackable)      //2
    {
        IsAttacking = true;
        OnHoldDownAttack?.Invoke(attackable);
    }



    void Update()
    {

        if (IsAttacking)
        {

            if (CanAttack(Attackable) == 1)
            {
                BiteAttack(Attackable);
            }

            if(CanAttack(Attackable) == 2)
            {
                HoldDownAttack(Attackable);
            }
        }
        else
        {
            if (CanAttack(Attackable) == 0)
                return;

            NoAttack(Attackable);
        }


    }

    protected virtual int CanAttack(Attackable attackable) => 0;
}
