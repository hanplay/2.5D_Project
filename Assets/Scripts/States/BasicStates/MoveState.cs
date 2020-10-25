using UnityEngine;

public class MoveState : BasicState
{
    private Rigidbody rigidbody;
    private StatsSystem statsSystem;

    public MoveState(Player player) : base(player)
    {
        rigidbody = player.GetComponent<Rigidbody>();
        statsSystem = player.GetStatsSystem();
    }

    public override void Begin() { }

    public override void TickAccept(float deltaTime, Command command)
    {
        animator.Play("Run");
        command.Visit(this);
    }

    public void MoveTo(Vector3 destination)
    {
        Vector3 direction = destination - player.GetPosition();
        direction.y = 0f;
        direction.Normalize();
        rigidbody.velocity = direction * statsSystem.GetTotalMoveSpeed();
    }

 
}
