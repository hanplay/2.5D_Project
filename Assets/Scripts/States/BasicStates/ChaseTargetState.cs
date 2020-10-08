using System.Collections.Generic;
using UnityEngine;
using GameUtility;

//this state must have nextState
public class ChaseTargetState : BasicState, ITargetExistsState
{
    private Rigidbody rigidbody;
    private float speed = 5f; //temperal value

  
    public ChaseTargetState(Player player) : base(player)
    {
        rigidbody = player.GetComponent<Rigidbody>();
    }

    public override void Begin()
    {
        animator.Play("Run");
    }


    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        Vector3 direction = targetUnit.GetPosition() - player.GetPosition();
        direction.Normalize();
        rigidbody.velocity = direction * speed;       
        
    }
    protected override bool IsEnded()
    {
        if (player.GetBaseAttackStrategy().GetRange() < player.DistanceToUnit(targetUnit))
            return false;
        else
            return true;
    }

    protected override void End() 
    {
        player.SetState(player.GetBaseAttackState(targetUnit));
        player.GetState().Begin();
        
    }



    public void SetCommand(Command command)
    {
        this.command = command;
    }

    public void OnTargetDead()
    {
        player.SetState(player.GetIdleState());
        player.GetState().Begin();
    }


    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }
}