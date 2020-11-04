using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNotifyDecorator : DamageStrategyDecorator
{
    public event EventHandler OnDamage;
    public DamageNotifyDecorator(IDamageStrategy damageStrategy) : base(damageStrategy) { }

    public override void Do(Unit targetUnit, int damage)
    {
        damageStrategy.Do(targetUnit, damage);
        OnDamage?.Invoke(this, EventArgs.Empty);
    }
}
