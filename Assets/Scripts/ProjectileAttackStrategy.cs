using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackStrategy : AttackStrategy
{

    private Projectile projectile;
    private List<Projectile> projectileList = new List<Projectile>();
    private int index;

    public ProjectileAttackStrategy(Unit owner, IDamageStrategy damageStrategy, float range, Projectile projectile) : base(owner, damageStrategy, range)
    {
        this.projectile = projectile;
    }

    public override void AnimationEventOccur()
    {
        while (true == projectileList[index].IsActive())
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
        projectileList[index].SetTarget(targetedUnit);
        int damage = owner.GetStatsSystem().GetTotalAttackPower();
        index = GetNextIndex(index);
    }


    private int GetNextIndex(int index)
    {
        if (projectileList.Count - 1 == index)
            return 0;
        else
            return index + 1;
    }

}
