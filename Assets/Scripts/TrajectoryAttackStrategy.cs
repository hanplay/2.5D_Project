using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class TrajectoryAttackStrategy : IAttackStrategy
{
    private Unit unit;
    private float range;
    private Animator animator;
    private StatsSystem statsSystem;
    private GameObject hitVisualEffect;
    private int animationHashCode;
    private Sprite trajectorySprite;

    public TrajectoryAttackStrategy(Unit unit, float range) 
    {
        this.unit = unit;
        animator = unit.transform.Find("model").GetComponent<Animator>();
        statsSystem = unit.GetStatsSystem();
        this.range = range;
    }

    public void SetTrajectorySprite(Sprite trajectorySprite)
    {
        this.trajectorySprite = trajectorySprite;
    }

    public void SetHitVisualEffect(GameObject hitVisualEffect)
    {
        this.hitVisualEffect = hitVisualEffect;
    }

    public void SetAnimationName(string animationName)
    {
        animationHashCode = Animator.StringToHash(animationName);
    }

    public void Attack(Unit targetUnit)
    {
        
        unit.BaseAttackAction = () =>
        {
            ActivateVisualEffect(targetUnit);
            Damage(targetUnit);
        };
        animator.Play(animationHashCode);
    }

    public float GetRange()
    {
        return range;
    }

    public void Damage(Unit targetUnit)
    {
        targetUnit.BeDamaged(statsSystem.GetTotalAttackPower());
    }

    public void ActivateVisualEffect(Unit targetUnit)
    {
        if (null == hitVisualEffect)
            return;
        GameObject.Instantiate(hitVisualEffect, targetUnit.GetPosition(), Quaternion.Euler(90f, 0f, 0f));
    }
}
