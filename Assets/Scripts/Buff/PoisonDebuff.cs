using UnityEngine;

public class PoisonDebuff : TimedBuff
{
    private float damagePeriod = 0.5f;
    private int trueDamage = 3;
    private float lagTime;
    private BasicFXVisualizer basicFXVisualizer;
    private IDamageStrategy trueDamageStrategy = new TrueDamageStrategy();

    public PoisonDebuff(BuffType TypeValue, float duration) : base(TypeValue, duration) { }
    
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
        lagTime += deltaTime;
        while(lagTime > damagePeriod)
        {
            lagTime -= damagePeriod;
            basicFXVisualizer.Paint(Color.green);
            trueDamageStrategy.Do(owner, trueDamage);
            Debug.Log("Debuff True Damage: " + trueDamage);
        }
        base.Tick(deltaTime);

    }

    public override void SetTargetUnit(Unit targetUnit)
    {
        base.SetTargetUnit(targetUnit);
        basicFXVisualizer = targetUnit.GetComponent<BasicFXVisualizer>();
    }
}
