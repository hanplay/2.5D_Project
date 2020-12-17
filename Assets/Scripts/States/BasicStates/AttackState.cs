using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BasicState, IMoveableState, IAttackableState
{
    public event StateSystem.MoveEventHandler OnMove;
    public event StateSystem.TargetUnitEventHandler OnChase;
    public AttackState(Unit owner, StateSystem stateSystem) : base(owner, stateSystem) 
    {
        attackSystem = owner.GetAttackSystem();
    }

    private AttackSystem attackSystem;

    public override void Begin()
    {
        base.Begin();
        Attack(targetedUnit);
    }

    public override void Tick(float deltaTime)
    {
        if(null == targetedUnit)
        {
            stateSystem.PopState();
        }
        if(true == attackSystem.InRange(targetedUnit))
        {
            attackSystem.GetAttackStrategy().Attack(targetedUnit);
        }
        else
        {
            OnChase.Invoke(this, targetedUnit);
        }
    }

    public void Attack(Unit targetedUnit)
    {
        this.targetedUnit = targetedUnit;        
    }

    public override bool IsTargetingState()
    {
        return true;
    }

    public override void ChaseTarget(Unit targetedUnit)
    {
        OnChase.Invoke(this, targetedUnit);
    }

    public override void MoveTo(Vector3 destination)
    {
        OnMove(this, destination);
    }
}
