﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantAttackStrategy : AttackStrategy
{
    public InstantAttackStrategy(Unit owner, IDamageStrategy damageStrategy, float range) : base(owner, damageStrategy, range) { }
    public override void AnimationEventOccur()
    {
        int damage = owner.GetStatsSystem().GetTotalAttackPower();
        damageStrategy.Do(targetedUnit, damage);
        targetedUnit = null;
    }

}

