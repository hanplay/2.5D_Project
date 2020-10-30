using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackStrategy : IAttackStrategy
{
    private Unit unit;
    private Animator animator;
    private StatsSystem statsSystem;
    protected GameObject hitVisualEffect;
    
    
    private float range = 1.5f;
    public MeleeAttackStrategy(Unit unit)
    {
        this.unit = unit;
        animator = unit.transform.Find("model").GetComponent<Animator>();
        statsSystem = unit.GetStatsSystem();
    }

    public void SetHitVisualEffect(GameObject hitVisualEffect)
    {
        this.hitVisualEffect = hitVisualEffect;
    }

    public float GetRange()
    {
        return range;
    }

    public void Attack(Unit targetUnit)
    {

        unit.BaseAttackAction = () =>
        {
            ActivateVisualEffect(targetUnit);
            Damage(targetUnit);
        };
        animator.Play("Attack");
    }

    public void ActivateVisualEffect(Unit targetUnit)
    {
        if (null == hitVisualEffect)
            return;
        GameObject.Instantiate(hitVisualEffect, targetUnit.GetPosition(), Quaternion.Euler(90f, 0f, 0f));       
    }

    public void Damage(Unit targetUnit)
    {
        Debug.Log("Dmg: " + statsSystem.GetTotalAttackPower());
        //targetUnit.BeDamaged(statsSystem.GetTotalAttackPower());
    }

}
