using UnityEngine.Assertions;
using GameUtility;

public class IdleState : BasicState
{
    
    public IdleState(Player player) : base(player) { }

    public override void Begin()
    {
        animator.Play("Idle");
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
