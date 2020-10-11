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
        moveState.MoveTo(targetUnit.GetPosition());
        if(player.DistanceToUnit(targetUnit) <= range)
        {
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
}
