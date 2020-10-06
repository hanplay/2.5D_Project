using System.Collections.Generic;
using UnityEngine;
using GameUtility;

//this state must have nextState
public class ChaseTargetState : BasicState 
{
    private Rigidbody rigidbody;
    private float speed = 5f; //temperal value

  
    public ChaseTargetState(Unit unit) : base(unit, StateType.Basic & StateType.TargetExist)
    {
        rigidbody = unit.GetComponent<Rigidbody>();
    }
    public override bool CanBegin()
    {
        return true;
    }
    public override void Begin()
    {
        animator.Play("Run");
    }


    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime); 
        Vector3 direction = unit.ToTargetUnitDirection();        
        direction.Normalize();
        rigidbody.velocity = direction * speed;       
        
    }
    protected override bool IsEnded()
    {
        if (null == GetNextState())
        {
            if (unit.GetBaseAttackState().IsTargetUnitInRange())
                return true;
        }
        return false;
    }

    protected override void End() 
    {
        unit.SetCurrentState(unit.GetBaseAttackState());
        unit.GetCurrentState().Begin();
        SetNextState(null);
    }

}