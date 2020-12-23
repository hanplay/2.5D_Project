using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDebuff : TimedBuff
{ 
    private float damagePeriod = 0.5f;
    private int trueDamage = 1;
    private float lagTime;
    private BasicFXVisualizer basicFXVisualizer;
    private IDamageStrategy trueDamageStrategy = new TrueDamageStrategy();
    private GameObject burnExplosion;



    public BurnDebuff(BuffType TypeValue, float duration, GameObject burnExplosion) : base(TypeValue, duration)
    {
        this.burnExplosion = burnExplosion;
        basicFXVisualizer = owner.GetComponent<BasicFXVisualizer>();
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
        basicFXVisualizer.Paint(Color.red);        
    }


    public override void EraseEffects()
    {
        basicFXVisualizer.Paint(Color.white);
    }

    public override int IndexNumber()
    {
        return currentStack;
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        if(5 == currentStack )
        {
            End();
            GameObject.Instantiate(burnExplosion, owner.GetPosition(), Quaternion.Euler(90f, 0f, 0f));
        }

        lagTime += deltaTime;
        while (lagTime > damagePeriod)
        {
            lagTime -= damagePeriod;
            basicFXVisualizer.Paint(Color.red);

            trueDamageStrategy.Do(owner, trueDamage * currentStack);
        }
    }
}
