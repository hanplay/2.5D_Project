using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageStrategyDecorator : DamageStrategy
{
    protected DamageStrategy damageStrategy;
    public readonly BuffType DecoratingBuffType;
    public DamageStrategyDecorator(DamageStrategy damageStrategy, BuffType DecoratingBuffType)
    {
        this.damageStrategy = damageStrategy;
        this.DecoratingBuffType = DecoratingBuffType;
    }

    public void SetDamageStrategy(DamageStrategy damageStrategy)
    {
        this.damageStrategy = damageStrategy;
    }

    public DamageStrategy GetDamageStrategy()
    {
        return damageStrategy;
    } 
}
