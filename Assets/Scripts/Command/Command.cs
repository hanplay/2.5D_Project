using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisitor
{
    void Visit(IdleState idleState);
    void Visit(MoveState moveState);
    void Visit(AttackState attackState);
    void Visit(SkillState skillState);
}

public abstract class Command : IVisitor
{
    protected Player player;

    protected Command(Player player)
    {
        this.player = player;
    }

    public abstract void Visit(IdleState idleState);
    public abstract void Visit(MoveState moveState);
    public abstract void Visit(AttackState attackState);
    public abstract void Visit(SkillState skillState);

}
