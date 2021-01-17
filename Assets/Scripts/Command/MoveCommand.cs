using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : Command
{
    private Vector3 destination;

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }

    public void Execute(Unit owner, Vector3 destination)
    {
        SetOwner(owner);
        SetDestination(destination);
        Execute();
    }

    public override void Execute()
    {
        IMoveableState moveableState = owner.GetStateSystem().GetCurrentState() as IMoveableState;
        if (null == moveableState)
            return;
        if(owner.GetTargetedUnitHandler().TryGetTargetedUnit(out Unit targetedUnit))
            owner.GetTargetedUnitHandler().ReleaseTarget();
        moveableState.MoveTo(destination);        
    }
}
