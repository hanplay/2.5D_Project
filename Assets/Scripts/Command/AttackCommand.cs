using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : Command
{
    private Unit targetUnit;
    private float range;

    public AttackCommand(Player player, Unit targetUnit) : base(player)
    {
        this.targetUnit = targetUnit;
        range = player.GetBaseAttackStrategy().GetRange();
    }


    public override void Visit(IdleState idleState)
    {
        if (player.DistanceToUnit(targetUnit) > range)
        {
            idleState.ChanegeToChaseState(targetUnit);
        }
        else
        {
            
            idleState.ChangeToAttackState(targetUnit);
        }
    }


    public override void Visit(MoveState moveState)
    {
        moveState.ChanegeToChaseState(targetUnit);
    }

    public override void Visit(ChaseState chaseState)
    {
        if (range >= player.DistanceToUnit(targetUnit))
            chaseState.ChangeToAttackState(targetUnit);
        
       
        if(targetUnit == chaseState.GetTargetUnit())
            chaseState.MoveToTargetUnit();
        
        //원래 타겟과 새로운 타겟이 다름
        chaseState.SetTargetUnit(targetUnit);
        chaseState.MoveToTargetUnit();
    }

    public override void Visit(AttackState attackState)
    {
        if(range < player.DistanceToUnit(targetUnit))
        {
            attackState.ChanegeToChaseState(targetUnit);
        }

        if (false == attackState.IsInDelayTime())
        {
            attackState.Begin();
        }
    }

    public override void Visit(SkillState skillState)
    {
        if (skillState.IsEnd())
        {
            skillState.ChangeToAttackState(targetUnit);
        }
    }

}
