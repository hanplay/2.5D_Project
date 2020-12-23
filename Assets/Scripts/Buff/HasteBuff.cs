using UnityEngine;

public class HasteBuff : TimedBuff
{
    public HasteBuff(BuffType TypeValue, float duration) : base(TypeValue, duration) { }


    public override void ApplyEffects()
    {
        owner.GetMoveSystem().AddSpeed(2f);
    }


    public override void EraseEffects()
    {
        owner.GetMoveSystem().AddSpeed(-2f);
    }

    public override int IndexNumber()
    {
        return DoNotShow;
    }
}
