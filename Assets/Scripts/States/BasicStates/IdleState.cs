using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class IdleState : BasicState, IMoveableState
{
    public event StateSystem.MoveEventHandler OnMove;
    public event StateSystem.TargetUnitEventHandler OnChase;

    //public IdleState(Unit player, StateSystem stateSystem) : base(player, stateSystem, Idle) { }

    public IdleState(Unit owner, StateSystem stateSystem) : base(owner, stateSystem)
    {
    }

    public override void Begin()
    {
        base.Begin();
    }

    public override void Tick(float deltaTime) 
    { animator.Play("Idle"); }

    public override void MoveTo(Vector3 destination)
    {
        OnMove.Invoke(this, destination);
    }

    public override void ChaseTarget(Unit targetUnit)
    {
        OnChase.Invoke(this, targetUnit);
    }

    public override bool IsTargetingState()
    {
        return false;
    }

}
