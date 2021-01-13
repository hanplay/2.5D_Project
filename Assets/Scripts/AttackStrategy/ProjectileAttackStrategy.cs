using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackStrategy : AttackStrategy
{    
    private ProjectileContainer projectileContainer;
    
    public ProjectileAttackStrategy(Unit owner, IDamageStrategy damageStrategy, float range, ProjectileContainer projectileContainer) : base(owner, damageStrategy, range)
    {
        this.projectileContainer = projectileContainer;
    }

    public override void AnimationEventOccur()
    {
        Projectile projectile = projectileContainer.GetNextProjectile();
        projectile.Launch(targetedUnit);
    }    
}
