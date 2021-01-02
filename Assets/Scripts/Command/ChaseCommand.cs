using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCommand : ICommand
{
    private Unit targetedUnit;
    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetedUnit = targetUnit;
    }

    public void Execute(Unit unit)
    {
        IMoveableState moveableState = unit.GetStateSystem().GetCurrentState() as IMoveableState;
        if (null == moveableState)
            return;

        if (true == unit.GetTargetedUnitHandler().TryGetTargetedUnit(out Unit handlerUnit))
        {
            if (handlerUnit != targetedUnit)            
                unit.GetTargetedUnitHandler().ReleaseTarget();            
        }
        else
            unit.GetTargetedUnitHandler().SetTarget(targetedUnit);

        moveableState.ChaseTarget(targetedUnit);
    }

    public void Execute(Unit unit, Unit targetUnit)
    {
        SetTargetUnit(targetUnit);
        Execute(unit);
    }
}
