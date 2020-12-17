using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMoveStrategy : IMoveStrategy
{
    private Unit mover;
    private AttackStrategy attackStrategy;

    public TeleportMoveStrategy(Unit mover)
    {
        this.mover = mover;       
    }


    public void ChaseTarget(Unit targetUnit)
    {
        
    }

    public void MoveTo(Vector3 destination)
    {
        
    }


}
