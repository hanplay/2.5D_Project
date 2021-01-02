using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy
{
    protected IDamageStrategy damageStrategy;

    protected Unit owner;
    protected Unit targetedUnit;
    protected Animator animator;

    private float baseRange;
    private float addedRange;
    private float totalRange;

    public void Attack(Unit targetedUnit)
    {
        animator.Play("Attack");
        this.targetedUnit = targetedUnit;
    }
    abstract public void AnimationEventOccur();

    public AttackStrategy(Unit owner, IDamageStrategy damageStrategy, float range)
    {
        this.owner = owner;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        this.damageStrategy = damageStrategy;
        baseRange = range;
        CalculateRange();
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
        return totalRange;
    }

    public void AddRange(float value)
    {
        addedRange += value;
        CalculateRange();
    }

    private void CalculateRange()
    {
        totalRange = baseRange + addedRange;
    }
}
