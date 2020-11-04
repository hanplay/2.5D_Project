using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStrategy : IDamageStrategy
{
    public void Do(Unit targetUnit, int healthPoints)
    {
        targetUnit.GetHealthPointsSystem().AddHealthPoints(healthPoints);
    }
}
