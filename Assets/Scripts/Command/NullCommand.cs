using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullCommand : Command
{
    public NullCommand(Player player) : base(player) { }

    public override void Visit(IdleState idleState) { }

    public override void Visit(MoveState moveState)
    {
        moveState.ChangeToIdleState();
    }

    public override void Visit(AttackState attackState)
    {
        attackState.InitializeLagTime();
        attackState.ChangeToIdleState();
    }

    public override void Visit(SkillState skillState)
    {
        if(skillState.IsEnd())
        {
            skillState.ChangeToIdleState();
        }        
    }
}
