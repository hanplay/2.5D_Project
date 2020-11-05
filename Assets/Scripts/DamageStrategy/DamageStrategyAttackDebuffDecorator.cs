using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStrategyAttackDebuffDecorator : DamageStrategyDecorator
{

    private Buff debuff;
  
    public DamageStrategyAttackDebuffDecorator(DamageStrategy damageStrategy, BuffType DecoratingBuffType, Buff debuff) :
        base(damageStrategy, DecoratingBuffType)
    {
        this.debuff = debuff;
    }

    public override void Do(Unit targetUnit, int damage)
    {
        damageStrategy.Do(targetUnit, damage);
        targetUnit.GetBuffSystem().AddBuff(debuff.Clone() as Buff);
    }
}