using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyPoisonBuff : TimedBuff
{
    private Buff attackDebuff;
    public DeadlyPoisonBuff(BuffType TypeValue, float duration, Buff attackDebuff) : base(TypeValue, duration)
    {
        this.attackDebuff = attackDebuff;
    }

    public override void ApplyEffects()
    {
        DamageStrategyAttackDebuffDecorator damageStrategyAttackDebuffDecorator =
            new DamageStrategyAttackDebuffDecorator(owner.GetAttackSystem().GetAttackStrategy().GetDamageStrategy(), TypeValue, attackDebuff);
        owner.GetAttackSystem().GetAttackStrategy().SetDamageStrategy(damageStrategyAttackDebuffDecorator);
    }

    public override void EraseEffects()
    {
        DamageStrategyDecorator damageStrategyDecorator = owner.GetAttackSystem().GetAttackStrategy().GetDamageStrategy() as DamageStrategyDecorator;
        if (damageStrategyDecorator.DecoratingBuffType == TypeValue)
        {
            owner.GetAttackSystem().GetAttackStrategy().SetDamageStrategy(damageStrategyDecorator.GetDamageStrategy());
        }
        else
        {

            DamageStrategyDecorator nextDamageStrategyDecorator = damageStrategyDecorator.GetDamageStrategy() as DamageStrategyDecorator;
            while (TypeValue != nextDamageStrategyDecorator.DecoratingBuffType)
            {
                damageStrategyDecorator = nextDamageStrategyDecorator;
                nextDamageStrategyDecorator = damageStrategyDecorator.GetDamageStrategy() as DamageStrategyDecorator;
            }
            damageStrategyDecorator.SetDamageStrategy(nextDamageStrategyDecorator.GetDamageStrategy());
        }
    }
    
    public override int IndexNumber()
    {
        return DoNotShow;
    }
}
