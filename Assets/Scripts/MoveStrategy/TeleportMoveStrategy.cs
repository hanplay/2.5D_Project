using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMoveStrategy : IMoveStrategy
{
    private Unit mover;
    private AttackSystem attackSystem;
    private GameObject teleportEffect;

    public TeleportMoveStrategy(Unit mover, GameObject teleportEffect)
    {
        this.mover = mover;
        attackSystem = mover.GetAttackSystem();
        this.teleportEffect = teleportEffect;
    }
    public TeleportMoveStrategy(Unit mover)
    {
        this.mover = mover;
        attackSystem = mover.GetAttackSystem();        
    }

    public void ChaseTarget(Unit targetedUnit)
    {
        float range = attackSystem.GetRange();
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


}
