﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackStrategy : IAttackStrategy
{
    private int attackNameHash;
    private Unit owner;
    private Unit targetUnit;
    private Animator animator;
    private IDamageStrategy damageStrategy;
    private float range;
    private Projectile projectile;
    private List<Projectile> projectileList = new List<Projectile>();
    private int index;

    public ProjectileAttackStrategy(Unit owner, Projectile projectile)
    {
        this.owner = owner;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        attackNameHash = Animator.StringToHash("Attack");
        this.projectile = projectile;      
    }

    public ProjectileAttackStrategy(Unit owner, string attackName, Projectile projectile)
    {
        this.owner = owner;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        attackNameHash = Animator.StringToHash(attackName);
        this.projectile = projectile;
    }
    public void Attack(Unit targetUnit)
    {
        animator.Play(attackNameHash);
    }

    public void AnimationEventOccur()
    {
        while(true == projectileList[index].IsActive())
        {
            if (projectileList.Count == index)
            {
                projectileList.Add(GameObject.Instantiate(projectile, owner.transform).GetComponent<Projectile>());
                projectileList[index].SetProjectileAttackStrategy(this);
            }
            else
                index = GetNextIndex(index);
        }
       
        
        projectileList[index].Show();
        projectileList[index].SetTargetUnit(targetUnit);
        int damage = owner.GetStatsSystem().GetTotalAttackPower();
        projectileList[index].SetDamage(damage);

        index = GetNextIndex(index);
    }

    public IDamageStrategy GetDamageStrategy()
    {
        return damageStrategy;
    }

    private int GetNextIndex(int index)
    {
        if (projectileList.Count - 1 == index)        
            return 0;        
        else
            return index + 1;
    }

    public void SetDamageStrategy(IDamageStrategy damageStrategy)
    {
        this.damageStrategy = damageStrategy;
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