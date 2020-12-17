using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMoveStrategy : IMoveStrategy
{
    private Unit mover;
    private Rigidbody rigidbody;
    private MoveSystem moveSystem;


    public StraightMoveStrategy(Unit mover, MoveSystem moveSystem)
    {
        this.mover = mover;
        rigidbody = mover.GetComponent<Rigidbody>();
        this.moveSystem = moveSystem;      
    }
    
    public void ChaseTarget(Unit targetUnit)
    {
        StraightMove(targetUnit.GetPosition());
    }

    public void MoveTo(Vector3 destination)
    {
        StraightMove(destination);
    }

    private void StraightMove(Vector3 destination)
    {
        Vector3 direction = destination - mover.GetPosition();
        direction.y = 0f;
        direction.Normalize();
        rigidbody.velocity = direction * moveSystem.GetSpeed();
    }
}
