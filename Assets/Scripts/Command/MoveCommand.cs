using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : ICommand
{
    private Vector3 destination;

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
    }

    public void Execute(Unit unit)
    {
        IMoveableState moveableState = unit.GetStateSystem().GetCurrentState() as IMoveableState;
        if (null == moveableState)
            return;
        if(unit.GetTargetedUnitHandler().TryGetTargetedUnit(out Unit targetedUnit))
            unit.GetTargetedUnitHandler().ReleaseTarget();
        moveableState.MoveTo(destination);
    }
    public void Execute(Unit unit, Vector3 destination)
    {
        SetDestination(destination);
        Execute(unit);
    }
}
