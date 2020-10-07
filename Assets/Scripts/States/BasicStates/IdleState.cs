using UnityEngine.Assertions;
using GameUtility;

public class IdleState : BasicState
{
    
    public IdleState(Player player) : base(player, StateType.Basic) { }

    public override void Begin()
    {
        animator.Play("Idle");
    }

    public override bool CanBegin()
    {
        return true;
    }

    protected override void End()
    {
        Assert.IsTrue(true, "Idle State End Call!");
    }

    protected override bool IsEnded()
    {
        return false;
    }
}
