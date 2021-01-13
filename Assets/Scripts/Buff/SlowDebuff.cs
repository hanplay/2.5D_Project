using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDebuff : TimedBuff
{
    private Color debuffColor = Color.blue;
    private float lagTime;
    private float renewalPeriod = 0.5f;
    private BasicFXVisualizer basicFXVisualizer;

    public SlowDebuff(BuffType TypeValue, float duration) : base(TypeValue, duration) { }

    public override void ApplyEffects()
    {
        basicFXVisualizer.Paint(debuffColor);
        owner.GetMoveSystem().AddSpeed(-0.5f);
    }

    public override void EraseEffects()
    {
        basicFXVisualizer.Paint(Color.white);
        owner.GetMoveSystem().AddSpeed(0.5f);
    }

    public override void Tick(float deltaTime)
    {
        lagTime += deltaTime;
        while (lagTime > renewalPeriod)
        {
            lagTime -= renewalPeriod;
            basicFXVisualizer.Paint(debuffColor);
        }
        base.Tick(deltaTime);
    }

    public override int IndexNumber()
    {
        return DoNotShow;
    }

    public override void SetTargetUnit(Unit targetUnit)
    {
        base.SetTargetUnit(targetUnit);
        basicFXVisualizer = owner.GetComponent<BasicFXVisualizer>();
    }
}
