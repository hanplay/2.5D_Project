using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueDamageStrategy : IDamageStrategy
{
    public void Do(Unit targetUnit, int damage)
    {
        targetUnit.GetHealthPointsSystem().SubtractHealthPoints(damage);        
    }


}
