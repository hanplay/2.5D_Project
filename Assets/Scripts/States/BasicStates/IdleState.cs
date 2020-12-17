using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class IdleState : State, IMoveableState
{
    public event StateSystem.MoveEventHandler OnMove;
    public event StateSystem.TargetUnitEventHandler OnChase;

    public IdleState(Unit player, StateSystem stateSystem) : base(player, stateSystem, Idle) { }

    public override void Begin()
    {
        base.Begin();
    }

    public override void Tick(float deltaTime) 
    { animator.Play("Idle"); }

    public void MoveTo(Vector3 destination)
    {
        OnMove.Invoke(this, destination);
    }

    public void ChaseTarget(Unit targetUnit)
    {
        OnChase.Invoke(this, targetUnit);
    }

    public override bool IsTargetingState()
    {
        return false;
    }

    
}
