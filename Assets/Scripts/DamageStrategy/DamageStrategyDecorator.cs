using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageStrategyDecorator : IDamageStrategy
{
    protected IDamageStrategy damageStrategy;

    public DamageStrategyDecorator(IDamageStrategy damageStrategy)
    {
        this.damageStrategy = damageStrategy;
    }

    public abstract void Do(Unit targetUnit, int damage);

    public IDamageStrategy GetDamageStrategy()
    {
        return damageStrategy;
    }
 
}
