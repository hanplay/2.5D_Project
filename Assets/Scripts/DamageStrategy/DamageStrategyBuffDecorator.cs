using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStrategyBuffDecorator : DamageStrategyDecorator, IDamageStrategy
{
    private Buff buff;
    public DamageStrategyBuffDecorator(IDamageStrategy damageStrategy, Buff buff) : base(damageStrategy)
    {
        this.buff = buff;
    }

    public override void Do(Unit targetUnit, int damage)
    {
        damageStrategy.Do(targetUnit, damage);
        targetUnit.GetBuffSystem().AddBuff(buff.Clone() as Buff);
    }
}
