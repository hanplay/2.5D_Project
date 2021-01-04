using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBuff : TimedBuff
{
    private GameObject stunEffect;
    private GameObject stunEffectInstance;
    private StunState stunState;

    public StunBuff(BuffType TypeValue, float duration, GameObject stunEffect) : base(TypeValue, duration)
    {
        this.stunEffect = stunEffect;
    }

    public override void ApplyEffects()
    {
        stunState = new StunState(owner, owner.GetStateSystem(), duration);
        stunEffectInstance = GameObject.Instantiate(stunEffect, owner.GetStatusEffectTransform());
        owner.GetStateSystem().PushState(stunState);
    }

    public override void EraseEffects()
    {
        if(owner.GetStateSystem().GetCurrentState() == stunState)
            owner.GetStateSystem().PopState();  
        GameObject.Destroy(stunEffectInstance);
    }

    public override int IndexNumber()
    {
        return DoNotShow;
    }
}
