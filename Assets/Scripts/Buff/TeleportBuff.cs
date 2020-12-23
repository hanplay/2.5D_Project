using UnityEngine;

public class TeleportBuff : TimedBuff
{
    private GameObject teleportEffect;
    public TeleportBuff(BuffType TypeValue, float duration, GameObject teleportEffect) : base(TypeValue, duration)
    {
        this.teleportEffect = teleportEffect;
    }
    
    public override void ApplyEffects()
    {
        owner.GetMoveSystem().SetMoveStrategy(new TeleportMoveStrategy(owner, teleportEffect));
    }

    public override void EraseEffects()
    {
        owner.GetMoveSystem().RevertBaseMoveStrategy();
    }

    public override int IndexNumber()
    {
        return DoNotShow;
    }
}
