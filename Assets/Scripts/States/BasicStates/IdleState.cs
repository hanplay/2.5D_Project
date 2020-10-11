

public class IdleState : BasicState
{
    public IdleState(Player player) : base(player)
    {
    }

    public override void Begin()
    {
        animator.Play("Idle");
        player.SetCommand(new NullCommand(player));
    }

    public override void Tick(float deltaTime, Command command) 
    {
        command.Visit(this);
    }
}
