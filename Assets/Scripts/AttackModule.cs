using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackModule : MonoBehaviour
{
    private Projectile projectile;
    private List<Projectile> projectileList;
    private DamageStrategy damageStrategy;

    void Awake()
    {
        if(TryGetComponent<Projectile>(out projectile))
        {
            projectileList = new List<Projectile>();
            projectileList.Add(projectile);
        }        
    }

    
}
