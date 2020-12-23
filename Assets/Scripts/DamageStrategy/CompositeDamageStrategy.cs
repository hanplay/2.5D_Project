using UnityEngine;

public class CompositeDamageStrategy : DamageStrategyDecorator
{
    private IDamageStrategy damageStrategy;
    public readonly BuffType DecoratingBuffType;
    private ITargetingStrategy targetingStrategy;
    private GameObject strikeEffect;
    private float radius;

    public CompositeDamageStrategy(IDamageStrategy damageStrategy, BuffType DecoratingBuffType, ITargetingStrategy targetingStrategy, GameObject strikeEffect, float radius) :
        base(damageStrategy, DecoratingBuffType)
    {
        this.damageStrategy = damageStrategy;
        this.targetingStrategy = targetingStrategy;
        this.strikeEffect = strikeEffect;
        this.radius = radius;
    }

    public override void Do(Unit targetUnit, int damage)
    {
        if (null != strikeEffect)        
            GameObject.Instantiate(strikeEffect, targetUnit.GetPosition(), Quaternion.Euler(90f, 0f, 0f));

        Collider[] colliders = Physics.OverlapSphere(targetUnit.GetPosition(), radius);        
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].TryGetComponent<Unit>(out Unit toDamageUnit))
            {       
                if (false == targetingStrategy.IsTargetable(toDamageUnit))
                    continue;       
                damageStrategy.Do(toDamageUnit, damage);
            }
        }
    }

}
