using System.Collections.Generic;
using UnityEngine;

//this state must have nextState
public class ChaseTargetState : BasicState 
{
    private Rigidbody rigidbody;
    private float speed = 5f; //temperal value

    public override bool CanBegin()
    {
        return true;
    }

    public ChaseTargetState(Unit unit) : base(unit)
    {
        rigidbody = unit.GetComponent<Rigidbody>();
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
            else
                return false;
        }
        else
            return false;
    }

    protected override void End() 
    {
        base.End();
        unit.SetState(unit.GetBaseAttackState());
        unit.GetState().Begin();
        SetNextState(null);
    }

}