using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCommand : Command
{
    public DieCommand(Player player) : base(player) { }

    public override void Visit(IdleState idleState)
    {
        idleState.ChangeToDieState();
    }

    public override void Visit(MoveState moveState)
    {
        moveState.ChangeToDieState();
    }

    public override void Visit(ChaseState chaseState)
    {
        chaseState.ChangeToDieState();
    }

    public override void Visit(AttackState attackState)
    {
        attackState.ChangeToDieState();
    }

    public override void Visit(DieState dieState)
    {
        return;
    }

    public override void Visit(SkillState skillState)
    {
        skillState.ChangeToDieState();
    }
}
