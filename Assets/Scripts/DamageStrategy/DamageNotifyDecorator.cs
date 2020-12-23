using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNotifyDecorator : DamageStrategyDecorator
{
    public event EventHandler OnDamage;
    public DamageNotifyDecorator(IDamageStrategy damageStrategy, BuffType DecoratingBuffType) : base(damageStrategy, DecoratingBuffType) { }    
  
    public override void Do(Unit targetUnit, int damage)
    {
        damageStrategy.Do(targetUnit, damage);
        OnDamage?.Invoke(this, EventArgs.Empty);
    }
}
