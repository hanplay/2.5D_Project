using UnityEngine;

public class HasteBuff : TimedBuff
{
    public HasteBuff(BuffType TypeValue, float duration) : base(TypeValue, duration) { }


    public override void ApplyEffects()
    {
        targetUnit.GetStatsSystem().AddMoveSpeed(2f);
    }


    public override void EraseEffects()
    {
        targetUnit.GetStatsSystem().AddMoveSpeed(-2f);
    }

    public override int IndexNumber()
    {
        return DoNotShow;
    }
}
