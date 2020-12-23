using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantAttackStrategy : AttackStrategy
{
    private int attackNameHash;
    private Unit owner;
    private Unit targetUnit;
    private Animator animator;

    public InstantAttackStrategy(Unit owner, IDamageStrategy damageStrategy)
    {
        this.owner = owner;
        this.damageStrategy = damageStrategy;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        attackNameHash = Animator.StringToHash("Attack");
    }

    public InstantAttackStrategy(Unit owner, IDamageStrategy damageStrategy, string attackName)
    {
        this.owner = owner;
        this.damageStrategy = damageStrategy;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        attackNameHash = Animator.StringToHash(attackName);
    }
    public override void Attack(Unit targetUnit)
    {
        animator.Play(attackNameHash);
        this.targetUnit = targetUnit;
    }

    public override void AnimationEventOccur()
    {
        int damage = owner.GetStatsSystem().GetTotalAttackPower();
        Debug.Log(string.Concat("TargetUnit: ", targetUnit, ", ",  damage));
        damageStrategy.Do(targetUnit, damage);
        targetUnit = null;
    }

}

