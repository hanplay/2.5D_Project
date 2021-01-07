using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMoveStrategy : IMoveStrategy
{
    private Unit mover;
    private GameObject teleportEffect;

    public TeleportMoveStrategy(Unit mover, GameObject teleportEffect)
    {
        this.mover = mover;
        this.teleportEffect = teleportEffect;
    }
    public TeleportMoveStrategy(Unit mover)
    {
        this.mover = mover;

    }

    public void ChaseTarget(Unit targetedUnit)
    {
        float range = mover.GetAttackStrategy().GetRange();
        Debug.Log("Teleprot: " + range);
        float shftingDistance = mover.DistanceToUnit(targetedUnit) - range + 0.1f;
        Vector3 direction = mover.DirectionToUnit(targetedUnit);
        Vector3 destination = mover.GetPosition() + direction * shftingDistance;
        TeleportMove(destination);
    }

    public void MoveTo(Vector3 destination)
    {
        TeleportMove(destination);
    }

    private void TeleportMove(Vector3 destination)
    {
        destination.y = mover.GetPosition().y;
        mover.transform.position = destination;
        if(null != teleportEffect)
            GameObject.Instantiate(teleportEffect, destination, Quaternion.Euler(90f, 0f, 0f));
    }

    public void ReverseChaseTarget(Unit targetedUnit)
    {
        ChaseTarget(targetedUnit);
    }
}
