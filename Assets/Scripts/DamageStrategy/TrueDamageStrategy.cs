using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueDamageStrategy : DamageStrategy
{
    public override void Do(Unit targetUnit, int damage)
    {
        targetUnit.GetHealthPointsSystem().SubtractHealthPoints(damage);        
    }


}
