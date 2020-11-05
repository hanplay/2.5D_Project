using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDamageStrategy : DamageStrategy
{
    public override void Do(Unit targetUnit, int damage)
    {
        int calculatedDamage;
        if(null == targetUnit)
        {
            Debug.Log("targetUnit is Null");
        }

        if(null == targetUnit.GetStatsSystem())
        {
            Debug.Log("StatsSystem is Null");
        }
        targetUnit.GetStatsSystem().CalculateDamage(damage, out calculatedDamage);
        targetUnit.GetHealthPointsSystem().SubtractHealthPoints(calculatedDamage);
    }


}