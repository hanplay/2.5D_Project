using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageStrategyDecorator : IDamageStrategy
{
    protected IDamageStrategy damageStrategy;
    public readonly BuffType DecoratingBuffType;
    public DamageStrategyDecorator(IDamageStrategy damageStrategy, BuffType DecoratingBuffType)
    {
        this.damageStrategy = damageStrategy;
        this.DecoratingBuffType = DecoratingBuffType;
    }

    public void SetDamageStrategy(IDamageStrategy damageStrategy)
    {
        this.damageStrategy = damageStrategy;
    }

    public IDamageStrategy GetDamageStrategy()
    {
        return damageStrategy;
    }

    public abstract void Do(Unit targetUnit, int damage);   
}
