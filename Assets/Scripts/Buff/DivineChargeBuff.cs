using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivineChargeBuff : TimedBuff
{
    private GameObject strikeEffect;
    private float radius;

    public DivineChargeBuff(BuffType TypeValue, GameObject strikeEffect, float radius, float duration) : base(TypeValue, duration)
    {
        this.strikeEffect = strikeEffect;
        if(null == strikeEffect)
        {
            Debug.Log("Strke Effect is null");
        }
        else
        {
            Debug.Log("Strike Effect is no null");
        }
        this.radius = radius;
    }

    public override void ApplyEffects()
    {
        owner.GetStatsSystem().AddAttackPower(1);
        CompositeDamageStrategy compositeDamageStrategy =
            new CompositeDamageStrategy(owner.GetAttackSystem().GetAttackStrategy().GetDamageStrategy(), BuffType.DivineCharge, owner.GetTargetingStrategy(), strikeEffect, radius);
        owner.GetAttackSystem().GetAttackStrategy().SetDamageStrategy(compositeDamageStrategy);
    }

    public override void EraseEffects()
    {
        owner.GetStatsSystem().AddAttackPower(-1);
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
