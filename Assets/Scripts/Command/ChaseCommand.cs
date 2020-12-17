using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCommand : ICommand
{
    private Unit targetUnit;
    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

    public void Execute(Unit unit)
    {
        IMoveableState moveableState = unit.GetStateSystem().GetCurrentState() as IMoveableState;
        if (null == moveableState)
            return;
        moveableState.ChaseTarget(targetUnit);
    }

    public void Execute(Unit unit, Unit targetUnit)
    {
        SetTargetUnit(targetUnit);
        Execute(unit);
    }
}
