using UnityEngine;

public class MoveState : BasicState
{
    private Rigidbody rigidbody;
    private float speed = 5f;

    public MoveState(Player player) : base(player)
    {
        rigidbody = player.GetComponent<Rigidbody>();
    }

    public override void Begin()
    {
        animator.Play("Run");
        
    }

    public override void TickAccept(float deltaTime, Command command)
    {
        command.Visit(this);
    }

    public void MoveTo(Vector3 destination)
    {
        Vector3 direction = destination - player.GetPosition();
        direction.y = 0f;
        direction.Normalize();
        rigidbody.velocity = direction * speed;
    }

    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

    public void RefreshUnitBuffer()
    {
        targetUnit = null;
    }


    public void MoveToTargetUnit()
    {
        Vector3 direction = targetUnit.GetPosition() - player.GetPosition();
        direction.y = 0f;
        direction.Normalize();
        rigidbody.velocity = direction * speed;
    }
  
}
