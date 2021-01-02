using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcealBuff : Buff
{
    private int multiple;
    public ConcealBuff(BuffType TypeValue, int multiple) : base(TypeValue)
    {
        this.multiple = multiple;
    }

    public override void ApplyEffects()
    {
        DamageNotifyDecorator damageNotifyDecorator = new DamageNotifyDecorator(owner.GetAttackStrategy().GetDamageStrategy(), TypeValue);
        owner.GetAttackStrategy().SetDamageStrategy(damageNotifyDecorator);
        
        int baseAttackPower = owner.GetStatsSystem().GetBaseAttackPower();
        owner.GetStatsSystem().AddAttackPower((multiple - 1) * baseAttackPower);
        owner.GetComponent<BasicFXVisualizer>().Paint(new Color(1f, 1f, 1f, 0.6f));

    }

    private void TargetUnit_OnAttackEndOrSkillBegin(object sender, System.EventArgs e)
    {
        EraseEffects();
    }

    public override void EraseEffects()
    {
        
        DamageStrategyDecorator damageStrategyDecorator = owner.GetAttackStrategy().GetDamageStrategy() as DamageStrategyDecorator;
        if(damageStrategyDecorator.DecoratingBuffType == TypeValue)
        {
            owner.GetAttackStrategy().SetDamageStrategy(damageStrategyDecorator.GetDamageStrategy());
        }
        else
        {

            DamageStrategyDecorator nextDamageStrategyDecorator = damageStrategyDecorator.GetDamageStrategy() as DamageStrategyDecorator;
            while(TypeValue != nextDamageStrategyDecorator.DecoratingBuffType)
            {
                damageStrategyDecorator = nextDamageStrategyDecorator;
                nextDamageStrategyDecorator = damageStrategyDecorator.GetDamageStrategy() as DamageStrategyDecorator;                
            }
            damageStrategyDecorator.SetDamageStrategy(nextDamageStrategyDecorator.GetDamageStrategy());
        }
        
        int baseAttackPower = owner.GetStatsSystem().GetBaseAttackPower();
        owner.GetStatsSystem().AddAttackPower(- (multiple  - 1) * baseAttackPower);
        owner.GetComponent<BasicFXVisualizer>().Paint(Color.white);
    }
    public override int IndexNumber()
    {
        return DoNotShow;
    }

    public override void Tick(float deltaTime) { }

    public override void Stack()
    {
        return;
    }
}
