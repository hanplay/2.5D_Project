using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDamageStrategy : IDamageStrategy
{
    public void Do(Unit targetUnit, int damage)
    {
        int calculatedDamage;
        targetUnit.GetStatsSystem().CalculateDamage(damage, out calculatedDamage);
        targetUnit.GetHealthPointsSystem().SubtractHealthPoints(calculatedDamage);
    }
}