using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem 
{
    private float baseRange;
    private float addedRange;
    private float totalRange;

    Unit owner;

    private AttackStrategy attackStrategy;

    public AttackSystem(Unit owner)
    {
        this.owner = owner;
    }

    public void Init(AttackStrategy attackStrategy, float range)
    {
        this.attackStrategy = attackStrategy;
        baseRange = range;
        CalculateRange();
    }
    
    public void AddRange(float value)
    {
        addedRange += value;
        CalculateRange();
    }

    public float GetRange()
    {
        return totalRange;            
    }

    public bool InRange(Unit targetedUnit)
    {
        if(totalRange < owner.DistanceToUnit(targetedUnit))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void CalculateRange()
    {
        totalRange = baseRange + addedRange;
    }

    public AttackStrategy GetAttackStrategy()
    {
        return attackStrategy;
    }
}