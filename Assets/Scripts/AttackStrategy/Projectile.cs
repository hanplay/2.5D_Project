using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Unit targetedUnit;

    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject hitVisualEffect;

    private ProjectileAttackStrategy projectileAttackStrategy;
    private StatsSystem statsSystem;

    void Update()
    {
        if (null == targetedUnit)
        {
            Hide();
        }

        if (0.5f > Vector3.Distance(transform.position, targetedUnit.GetPosition()))
        {
            projectileAttackStrategy.GetDamageStrategy().Do(targetedUnit, statsSystem.GetTotalAttackPower());
            Instantiate(hitVisualEffect);
            transform.localPosition = Vector3.zero;
            targetedUnit = null;
            Hide();
        }
        Vector3 direction = targetedUnit.GetPosition() - transform.position;
        direction.Normalize();
        transform.Translate(Time.deltaTime * direction * speed);
    }

    public void Init(ProjectileAttackStrategy projectileAttackStrategy, StatsSystem statsSystem )
    {
        this.projectileAttackStrategy = projectileAttackStrategy;
        this.statsSystem = statsSystem;

    }

    public void SetProjectileAttackStrategy(ProjectileAttackStrategy projectileAttackStrategy)
    {
        this.projectileAttackStrategy = projectileAttackStrategy;
    }

    public void SetTarget(Unit targetedUnit)
    {
        this.targetedUnit = targetedUnit;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
