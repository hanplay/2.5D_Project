using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDebuff : TimedBuff
{
    private float periodicLagTime;
    private float damagePeriod;
    private int trueDamage;

    private GameObject burnExplosion;



    public BurnDebuff(BuffType TypeValue, float duration, GameObject burnExplosion) : base(TypeValue, duration)
    {
        this.burnExplosion = burnExplosion;
    }

    public override void ApplyEffects()
    {
        
        
    }

    public override object Clone()
    {
        return new BurnDebuff(TypeValue, duration, burnExplosion);
        
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
        targetUnit.BeTrueDamaged(trueDamage * currentStack);
        if(5 ==currentStack )
        {
            End();
            GameObject.Instantiate(burnExplosion, targetUnit.GetPosition(), Quaternion.Euler(90f, 0f, 0f));
        }

    }
}
