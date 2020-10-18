using UnityEngine;

public class AttackState : BasicState, ITargetExistsState
{
    private float duration;
    private float lagTime;

    public AttackState(Player player) : base(player) { }

    public override void Begin()
    {
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

    public void InitializeLagTime()
    {
        lagTime = 0f;
    }

    private void Work()
    {
        Debug.Log("Damage!");
    }

    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }
}
