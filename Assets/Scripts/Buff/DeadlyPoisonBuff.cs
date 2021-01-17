using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyPoisonBuff : TimedBuff
{
    private Buff attackDebuff;
    private RemoveDamageStrategeDecoratorCommand removeDamageStrategeDecoratorCommand = new RemoveDamageStrategeDecoratorCommand();
    public DeadlyPoisonBuff(BuffType TypeValue, float duration, Buff attackDebuff) : base(TypeValue, duration)
    {
        this.attackDebuff = attackDebuff;
    }

    public override void ApplyEffects()
    {
        DamageStrategyAttackDebuffDecorator damageStrategyAttackDebuffDecorator =
            new DamageStrategyAttackDebuffDecorator(owner.GetAttackStrategy().GetDamageStrategy(), TypeValue, attackDebuff);
        owner.GetAttackStrategy().SetDamageStrategy(damageStrategyAttackDebuffDecorator);
    }

    public override void EraseEffects()
    {
        removeDamageStrategeDecoratorCommand.Execute(owner, TypeValue);
    }
    
    public override int IndexNumber()
    {
        return DoNotShow;
    }
}
