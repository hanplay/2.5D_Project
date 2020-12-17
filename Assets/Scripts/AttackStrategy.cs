using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy 
{
    protected DamageStrategy damageStrategy;

    abstract public void Attack(Unit targetUnit);
    abstract public void AnimationEventOccur();
    public void SetDamageStrategy(DamageStrategy damageStrategy)
    {
        this.damageStrategy = damageStrategy;
    } 

    public DamageStrategy GetDamageStrategy()
    {
        return damageStrategy;
    }
}

