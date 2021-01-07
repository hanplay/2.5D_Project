using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMoveStrategy : IMoveStrategy
{
    private Unit mover;
    private Rigidbody rigidbody;
    private MoveSystem moveSystem;
    private float closeDistance = 1.5f;

    public StraightMoveStrategy(Unit mover)
    {
        this.mover = mover;
        rigidbody = mover.GetComponent<Rigidbody>();
        moveSystem = mover.GetMoveSystem();     
    }
    
    public void ChaseTarget(Unit targetedUnit)
    {
        StraightMove(targetedUnit.GetPosition());
    }

    public void MoveTo(Vector3 destination)
    {
        StraightMove(destination);
    }

    public void ReverseChaseTarget(Unit targetedUnit)
    {
        Vector3 direction = mover.GetPosition() - targetedUnit.GetPosition();
        direction.y = 0f;
        direction.Normalize();
        rigidbody.velocity = direction * moveSystem.GetSpeed();
    }

    private void StraightMove(Vector3 destination)
    {
        Vector3 direction = destination - mover.GetPosition();
        direction.y = 0f;
        direction.Normalize();
        rigidbody.velocity = direction * moveSystem.GetSpeed();
    }
}
