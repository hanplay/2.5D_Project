using UnityEngine;

public class MoveCommand : Command
{
    private Vector3 destination;
    public MoveCommand(Player player, Vector3 destination) : base(player)
    {
        this.destination = destination;
    }
    public override void Visit(IdleState idleState)
    {
        idleState.ChangeToMoveState();
    }

    public override void Visit(MoveState moveState)
    {
        moveState.SetTargetUnit(null);
        moveState.MoveTo(destination);
        if(0.8f >Vector3.Distance(player.GetPosition(), destination))
        {
            moveState.ChangeToIdleState();
        }
    }

    public override void Visit(AttackState attackState)
    {
        attackState.InitializeLagTime();
        player.SetState(player.GetMoveState());
        player.GetMoveState().SetTargetUnit(null);
        player.GetState().Begin();
        
    }

    public override void Visit(SkillState skillState)
    {
        return;
    }

}
