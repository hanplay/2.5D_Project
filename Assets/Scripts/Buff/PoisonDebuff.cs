using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonDebuff : TimedBuff
{
    private float damagePeriod = 0.1f;
    private int trueDamage = 1;
    private float lagTime;
    private BasicFXVisualizer basicFXVisualizer;

    public PoisonDebuff(BuffType TypeValue, float duration) : base(TypeValue, duration)
    {
        basicFXVisualizer = targetUnit.GetComponent<BasicFXVisualizer>();
    }

    public void SetDamagePeriod(float period)
    {
        damagePeriod = period;
    }

    public void SetTrueDamagePerPeriod(int trueDamage)
    {
        this.trueDamage = trueDamage;
    }
    

    public override void ApplyEffects()
    {
        basicFXVisualizer.Paint(Color.green);
    }


    public override void EraseEffects()
    {
        basicFXVisualizer.Paint(Color.white);
    }

    public override int IndexNumber()
    {
        return DoNotShow;
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        lagTime += deltaTime;
        while(lagTime > damagePeriod)
        {
            lagTime -= damagePeriod;
            basicFXVisualizer.Paint(Color.green);
            targetUnit.BeTrueDamaged(trueDamage);
        }

    }
}
