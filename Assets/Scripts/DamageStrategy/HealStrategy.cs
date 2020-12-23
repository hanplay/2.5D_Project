using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStrategy : IDamageStrategy
{
    private GameObject healEffect;
    public HealStrategy() { }
    public HealStrategy(GameObject healEffect)
    {
        this.healEffect = healEffect;
    }
    public void Do(Unit targetUnit, int healthPoints)
    {        
        if(null != healEffect)
            GameObject.Instantiate(healEffect, targetUnit.GetPosition(), Quaternion.Euler(90f, 0f, 0f));
        targetUnit.GetHealthPointsSystem().AddHealthPoints(healthPoints);
    }
}
