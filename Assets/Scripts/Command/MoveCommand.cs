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
        moveState.MoveTo(destination);
        if(0.8f >Vector3.Distance(player.GetPosition(), destination))
        {
            moveState.ChangeToIdleState();
        }
    }

    public override void Visit(AttackState attackState)
    {
        attackState.InitializeLagTime();
        attackState.ChangeToMoveState();
    }
}
