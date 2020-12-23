using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BasicState, IMoveableState
{
    public event StateSystem.TargetUnitEventHandler OnChase;

    private Vector3 destination;
    private MoveSystem moveSystem;

    public MoveState(Unit owner, StateSystem stateSystem) : base(owner, stateSystem) 
    {
        moveSystem = owner.GetMoveSystem();
    }

    public override void Begin()
    {
        base.Begin();
        animator.Play("Run");
    }

    public override void Tick(float deltaTime)
    {
        if (0.8f > Vector3.Distance(owner.GetPosition(), destination))
            stateSystem.PopState();
        moveSystem.GetUsingMoveStrategy().MoveTo(destination);
    }

    public override void ChaseTarget(Unit targetUnit)
    {
        OnChase(this, targetUnit);
    }

    public override void MoveTo(Vector3 destination)
    {
        owner.FlipToTarget(destination);
        this.destination = destination;
    }
}
