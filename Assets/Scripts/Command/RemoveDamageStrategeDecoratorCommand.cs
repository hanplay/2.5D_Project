using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDamageStrategeDecoratorCommand : Command
{
    private BuffType buffType;
    public void Execute(Unit owner, BuffType buffType)
    {
        SetOwner(owner);
        SetBuffType(buffType);
        Execute();
    }

    public override void Execute()
    {
        while (true)
        {
            DamageStrategyDecorator damageStrategyDecorator = owner.GetAttackStrategy().GetDamageStrategy() as DamageStrategyDecorator;
            if (null == damageStrategyDecorator)
                return;
            if (buffType == damageStrategyDecorator.DecoratingBuffType)
            {
                IDamageStrategy damageStrategy = damageStrategyDecorator.GetDamageStrategy();
                owner.GetAttackStrategy().SetDamageStrategy(damageStrategy);
            }
        }
    }

    void SetBuffType(BuffType buffType)
    {
        this.buffType = buffType;
    }
}
