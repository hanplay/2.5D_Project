using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStrategy : DamageStrategy
{
    public override void Do(Unit targetUnit, int healthPoints)
    {
        targetUnit.GetHealthPointsSystem().AddHealthPoints(healthPoints);
    }
}
