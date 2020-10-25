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
        targetUnit.OnAttackEnd += TargetUnit_OnAttackEndOrSkillBegin;
        
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
        targetUnit.OnAttackEnd -= TargetUnit_OnAttackEndOrSkillBegin;
        int baseAttackPower = targetUnit.GetStatsSystem().GetBaseAttackPower();
        targetUnit.GetStatsSystem().AddAttackPower(- (multiple  - 1) * baseAttackPower);
        targetUnit.GetComponent<BasicFXVisualizer>().Paint(Color.white);
    }
    public override object Clone()
    {
        throw new System.NotImplementedException();
    }

    public override int IndexNumber()
    {
        return DoNotShow;
    }

    public override void Tick(float deltaTime)
    {
        
    }
}
