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
        if(player.DistanceToUnit(targetUnit) > range)
        {
            idleState.ChangeToMoveState();
        }
        else
        {
            idleState.ChangeToAttackState();
        }
    }


    public override void Visit(MoveState moveState)
    {
        moveState.SetTargetUnit(targetUnit);
        if (range < player.DistanceToUnit(targetUnit))
            moveState.MoveToTargetUnit();
        else
        {
            moveState.RefreshUnitBuffer();
            moveState.ChangeToAttackState();
        }
    }


    public override void Visit(AttackState attackState)
    {
        if(false == attackState.IsInDelayTime())
        {
            attackState.InitializeLagTime();
            attackState.Begin();
        }
    }

    public override void Visit(SkillState skillState)
    {
        if (skillState.IsEnd())
            skillState.ChangeToAttackState();
    }
}
