using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDebuff : TimedBuff
{
    private float periodicLagTime;
    private float damagePeriod;
    private int trueDamage;

    private GameObject explosion; // prefab



    public BurnDebuff(BuffType TypeValue, float duration) : base(TypeValue, duration)
    {

    }

    public override void ApplyEffects()
    {
        
    }

    public override object Clone()
    {
        return new BurnDebuff(TypeValue, duration);
        
    }

    public override void EraseEffects()
    {
        
    }

    public override int IndexNumber()
    {
        return currentStack;
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        

    }
}
