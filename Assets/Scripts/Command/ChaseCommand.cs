using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCommand : Command
{
    private Unit targetedUnit;
    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetedUnit = targetUnit;
    }

    public void Execute(Unit owner, Unit targetUnit)
    {
        SetOwner(owner);
        SetTargetUnit(targetUnit);
        Execute();
    }

    public override void Execute()
    {
        IMoveableState moveableState = owner.GetStateSystem().GetCurrentState() as IMoveableState;
        if (null == moveableState)
            return;

        if (true == owner.GetTargetedUnitHandler().TryGetTargetedUnit(out Unit handlerUnit))
        {
            if (handlerUnit != targetedUnit)
            {
                owner.GetTargetedUnitHandler().ReleaseTarget();
                owner.GetTargetedUnitHandler().SetTarget(targetedUnit);
            }
        }
        else
            owner.GetTargetedUnitHandler().SetTarget(targetedUnit);

        moveableState.ChaseTarget(targetedUnit);
    }
}
