using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantAttackStrategy : IAttackStrategy
{
    private int attackNameHash;
    private Unit owner;
    private Unit targetUnit;
    private Animator animator;
    private float range;
    private DamageStrategy damageStrategy;
    
    public InstantAttackStrategy(Unit owner, DamageStrategy damageStrategy, float range)
    {
        this.owner = owner;
        this.damageStrategy = damageStrategy;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        attackNameHash = Animator.StringToHash("Attack");
        this.range = range;
    }

    public InstantAttackStrategy(Unit owner, DamageStrategy damageStrategy, string attackName, float range)
    {
        this.owner = owner;
        this.damageStrategy = damageStrategy;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        attackNameHash = Animator.StringToHash(attackName);
        this.range = range;
    }

    public void Attack(Unit targetUnit)
    {
        animator.Play(attackNameHash);
        this.targetUnit = targetUnit;
    }

    public void AnimationEventOccur()
    {
        int damage = owner.GetStatsSystem().GetTotalAttackPower();
        Debug.Log("TargetUnit: " + targetUnit);
        damageStrategy.Do(targetUnit, damage);
        targetUnit = null;        
    }
    
    public void SetDamageStrategy(DamageStrategy damageStrategy)
    {
        this.damageStrategy = damageStrategy;
    }

    public DamageStrategy GetDamageStrategy()
    {
        return damageStrategy;
    }

    public float GetRange()
    {
        return range;
    }

    public void SetRange(float range)
    {
        this.range = range;
    }
}

