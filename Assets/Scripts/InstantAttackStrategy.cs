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
    private IDamageStrategy damageStrategy;
    
    public InstantAttackStrategy(Unit owner, IDamageStrategy damageStrategy, float range)
    {
        this.owner = owner;
        this.damageStrategy = damageStrategy;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        attackNameHash = Animator.StringToHash("Attack");
        this.range = range;
    }

    public InstantAttackStrategy(Unit owner, IDamageStrategy damageStrategy, string attackName, float range)
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
        damageStrategy.Do(targetUnit, damage);
        targetUnit = null;        
    }
    
    public void SetDamageStrategy(IDamageStrategy damageStrategy)
    {
        this.damageStrategy = damageStrategy;
    }

    public IDamageStrategy GetDamageStrategy()
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

