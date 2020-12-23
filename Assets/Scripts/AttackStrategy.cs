using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy 
{
    protected IDamageStrategy damageStrategy;

    abstract public void Attack(Unit targetUnit);
    abstract public void AnimationEventOccur();
    public void SetDamageStrategy(IDamageStrategy damageStrategy)
    {
        this.damageStrategy = damageStrategy;
    } 

    public IDamageStrategy GetDamageStrategy()
    {
        return damageStrategy;
    }
}

