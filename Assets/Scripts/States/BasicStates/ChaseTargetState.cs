using System.Collections.Generic;
using UnityEngine;
using GameUtility;

//this state must have nextState
public class ChaseTargetState : BasicState 
{
    private Rigidbody rigidbody;
    private float speed = 5f; //temperal value

  
    public ChaseTargetState(Player player) : base(player, StateType.Basic & StateType.TargetExist)
    {
        rigidbody = player.GetComponent<Rigidbody>();
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
        Vector3 direction = player.ToTargetUnitDirection();        
        direction.Normalize();
        rigidbody.velocity = direction * speed;       
        
    }
    protected override bool IsEnded()
    {
        if (null == GetNextState())
        {
            if (player.GetBaseAttackState().IsTargetUnitInRange())
                return true;
        }
        return false;
    }

    protected override void End() 
    {
        player.SetCurrentState(player.GetBaseAttackState());
        player.GetCurrentState().Begin();
        SetNextState(null);
    }

}