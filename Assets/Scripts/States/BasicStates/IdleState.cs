

public class IdleState : BasicState
{
    public IdleState(Player player) : base(player)
    {
    }

    public override void Begin()
    {
        animator.Play("Idle");
        inputBuffer.Refresh();
    }

    public override void Tick(float deltaTime)
    {
        if (InputType.Attack == inputBuffer.GetInputType())
        {
            if (player.GetBaseAttackStrategy().GetRange() < player.DistanceToUnit(inputBuffer.GetTargetUnit()))
            {
                player.SetState(player.GetMoveState());
                player.GetState().Begin();
            }
            else
            {
                player.SetState(player.GetAttackState());
                player.GetState().Begin();
            }
        }
        else if (InputType.Move == inputBuffer.GetInputType())
        {
            player.SetState(player.GetMoveState());
            player.GetState().Begin();
        }
        else if (InputType.Skill == inputBuffer.GetInputType())
        {
            //
        }

    }
}
