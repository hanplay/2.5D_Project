using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivineChargeBuff : TimedBuff
{
    private GameObject strikeEffect;
    private float radius;
    private RemoveDamageStrategeDecoratorCommand removeDamageStrategeDecoratorCommand = new RemoveDamageStrategeDecoratorCommand();

    public DivineChargeBuff(BuffType TypeValue, GameObject strikeEffect, int maxStack, float radius, float duration) : base(TypeValue, duration)
    {
        this.strikeEffect = strikeEffect;
        this.radius = radius;
        this.maxStack = maxStack;
        currentStack = maxStack;
    }

    public override void ApplyEffects()
    {
        owner.GetStatsSystem().AddAttackPower(5);        

        CompositeDamageStrategy compositeDamageStrategy =
            new CompositeDamageStrategy(owner.GetAttackStrategy().GetDamageStrategy(), TypeValue, owner.GetTargetingStrategy(), strikeEffect, radius);

        DamageNotifyDecorator damageNotifyDecorator = new DamageNotifyDecorator(compositeDamageStrategy, BuffType.DivineCharge);
        damageNotifyDecorator.OnDamage += DamageNotifyDecorator_OnDamage;

        owner.GetAttackStrategy().SetDamageStrategy(damageNotifyDecorator);
    }

    private void DamageNotifyDecorator_OnDamage(object sender, System.EventArgs e)
    {
        currentStack -= 1;
    }

    public override void EraseEffects()
    {
        owner.GetStatsSystem().AddAttackPower(-5);
        removeDamageStrategeDecoratorCommand.Execute(owner, TypeValue);
    }

    public override int IndexNumber()
    {
        return currentStack;
    }

    public override void Stack()
    {
        base.Stack();
        currentStack = maxStack;
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        if(0 == currentStack)
        {
            End();
        }
    }
}
