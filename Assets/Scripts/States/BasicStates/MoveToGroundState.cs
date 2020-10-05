using GameUtility;
using UnityEngine;

public class MoveToGroundState : BasicState
{
    private Vector3 destination;
    private Rigidbody rigidbody;

    private float speed = 5f; //temperal value
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }
    public MoveToGroundState(Unit unit) : base(unit, StateType.Basic) 
    {
        rigidbody = unit.GetComponent<Rigidbody>();
    }
    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        Vector3 toDestinationDirection = destination - unit.GetPosition();
        toDestinationDirection.y = 0f;
        toDestinationDirection.Normalize();
        rigidbody.velocity = toDestinationDirection * speed;

        if (0 > toDestinationDirection.x)
            unit.FlipLeft();
        if (0 < toDestinationDirection.x)
            unit.FlipRight();
    }

    public override bool CanBegin()
    {
        return true;
    }


    public override void Begin()
    {
        animator.Play("Run");
    }

    protected override void End()
    {
        unit.SetState(unit.GetIdleState());
        unit.GetState().Begin();
        SetNextState(null);
    }

    protected override bool IsEnded()
    {
        if (0.8f < Vector3.Distance(destination, unit.GetPosition()))
        {
            return false;
        }
        else
            return true;
    }

}
