using UnityEngine;

public class HasteBuff : TimedBuff
{
    public HasteBuff(Unit targetUnit, float duration) : base(targetUnit, duration) 
    {
        this.duration = duration;
    }

    public override void ApplyEffects()
    {
        targetUnit.GetStatsSystem().AddMoveSpeed(2f);
        Debug.Log("Total Speed plus!");
    }

    public override object Clone()
    {
        return new HasteBuff(targetUnit, duration);
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
