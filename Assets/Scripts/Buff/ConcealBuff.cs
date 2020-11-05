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
        DamageNotifyDecorator damageNotifyDecorator = new DamageNotifyDecorator(targetUnit.GetAttackStrategy().GetDamageStrategy(), TypeValue);
        targetUnit.GetAttackStrategy().SetDamageStrategy(damageNotifyDecorator);
        
        int baseAttackPower = targetUnit.GetStatsSystem().GetBaseAttackPower();
        targetUnit.GetStatsSystem().AddAttackPower((multiple - 1) * baseAttackPower);
        targetUnit.GetComponent<BasicFXVisualizer>().Paint(new Color(1f, 1f, 1f, 0.6f));

    }

    private void TargetUnit_OnAttackEndOrSkillBegin(object sender, System.EventArgs e)
    {
        EraseEffects();
    }

    public override void EraseEffects()
    {
        
        DamageStrategyDecorator damageStrategyDecorator = targetUnit.GetAttackStrategy().GetDamageStrategy() as DamageStrategyDecorator;
        if(damageStrategyDecorator.DecoratingBuffType == TypeValue)
        {
            targetUnit.GetAttackStrategy().SetDamageStrategy(damageStrategyDecorator.GetDamageStrategy());
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
        
        int baseAttackPower = targetUnit.GetStatsSystem().GetBaseAttackPower();
        targetUnit.GetStatsSystem().AddAttackPower(- (multiple  - 1) * baseAttackPower);
        targetUnit.GetComponent<BasicFXVisualizer>().Paint(Color.white);
    }
    public override int IndexNumber()
    {
        return DoNotShow;
    }

    public override void Tick(float deltaTime) { }
}
