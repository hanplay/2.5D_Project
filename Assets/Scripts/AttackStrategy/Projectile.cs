using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Unit owner;
    private Unit targetedUnit;

    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject hitVisualEffect;
    private Transform projectileContainerTransform;

    private void Awake()
    {
        Hide();
    }

    void Update()
    {
        if (null == targetedUnit)
        {
            StoreInProjectileContainer();
        }

        Vector3 projectilePosition = transform.position;
        Vector3 targetPosition = targetedUnit.GetPosition();

        if (0.5f > Vector3.Distance(projectilePosition, targetPosition))
        {      
            
            Instantiate(hitVisualEffect, targetedUnit.GetPosition(), Quaternion.identity);
            transform.localPosition = Vector3.zero;
            if(null == owner)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                owner.GetAttackStrategy().GetDamageStrategy().Do(targetedUnit, owner.GetStatsSystem().GetTotalAttackPower());
                StoreInProjectileContainer();
                return;
            }
        }
        Vector3 direction = targetPosition - projectilePosition;
        direction.Normalize();
        transform.Translate(Time.deltaTime * direction * speed, Space.World);
        float angle = Mathf.Atan2(direction.z / Mathf.Sqrt(2), direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(45f, 0f, angle);
    }
    
    public void Init(Unit owner, Transform projectileContainerTransform)
    {
        this.owner = owner;
        this.projectileContainerTransform = projectileContainerTransform;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void StoreInProjectileContainer()
    {
        targetedUnit = null;
        gameObject.SetActive(false);
        transform.SetParent(projectileContainerTransform);
        transform.localPosition = Vector3.zero;
    }

    public void Launch(Unit targetedUnit)
    {
        gameObject.SetActive(true);
        transform.SetParent(null);
        this.targetedUnit = targetedUnit;
    }

}
