using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContainer : MonoBehaviour
{
    Unit owner;
    [SerializeField]
    private Projectile projectileSample;

    private void Awake()
    {
        owner = transform.parent.GetComponent<Unit>();
    }
   
    public Projectile GetNextProjectile()
    {
        if(0 == transform.childCount)
        {
            Projectile projectile = Instantiate(projectileSample, transform.position, Quaternion.identity, transform);
            projectile.Init(owner, transform);
            return projectile;
        }
        else
        {
            return transform.GetChild(0).GetComponent<Projectile>();
        }
    }
}
