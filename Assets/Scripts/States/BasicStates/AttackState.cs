using System;
using UnityEngine;

public class AttackState : BasicState, ITargetExistsState
{
    private float duration;
    private float lagTime;


    public AttackState(Player player) : base(player) { }

    public override void Begin()
    {
        lagTime = 0f;
        player.OnAttackBeginNotify();
        player.BaseAttackAction = Work;
        animator.Play("Attack");
        if(0 == duration)
            duration = player.GetClipLength("Attack");
    }

    public override void TickAccept(float deltaTime, Command command)
    {
        command.Visit(this);
        lagTime += deltaTime;
    }

    public bool IsInDelayTime()
    {
        if (lagTime < duration)
            return true;
        else
            return false;
    }

    private void Work()
    {
        Debug.Log("Damage!");
        player.OnAttackEndNotify();
    }

    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }
    
}
