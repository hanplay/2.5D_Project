using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayBuff : AuraBuff
{
    public PrayBuff(BuffType TypeValue) : base(TypeValue) { }
    public PrayBuff(BuffType TypeValue, float cycleDuration) : base(TypeValue, cycleDuration) { }
 
    public override void ApplyEffects()
    {
        owner.GetStatsSystem().AddArmor(2);
        owner.GetStatsSystem().AddAttackPower(3);
        owner.GetMoveSystem().AddSpeed(1);
    }

    public override void EraseEffects()
    {
        owner.GetStatsSystem().AddArmor(-2);
        owner.GetStatsSystem().AddAttackPower(-3);
        owner.GetMoveSystem().AddSpeed(-1);
    }
}
