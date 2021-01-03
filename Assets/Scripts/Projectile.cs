//using System;
//using UnityEngine;

//public class Projectile : MonoBehaviour
//{
//    private Unit owner;
//    private int damage;
//    [SerializeField]
//    private float speed;
//    [SerializeField]
//    private GameObject hitVisualEffect;


//    void Update()
//    {
//        if (null == targetUnit)
//        {
//            Hide();
//        }

//        if(0.5f > Vector3.Distance(transform.position, targetUnit.GetPosition()))
//        {
//            projectileAttackStrategy.GetDamageStrategy().Do(targetUnit, damage);
//            Instantiate(hitVisualEffect);
//            transform.localPosition = Vector3.zero;
//            targetUnit = null;
//            Hide();
//        }
//        Vector3 direction = targetUnit.GetPosition() - transform.position;
//        direction.Normalize();
//        transform.Translate(Time.deltaTime * direction * speed);
//    }

//    public void SetProjectileAttackStrategy(ProjectileAttackStrategy projectileAttackStrategy)
//    {
//        this.projectileAttackStrategy = projectileAttackStrategy;
//    }


//    public void SetDamage(int damage)
//    {
//        this.damage = damage;
//    }

//    public void Show()
//    {
//        gameObject.SetActive(true);
//    }

//    public void Hide()
//    {
//        gameObject.SetActive(false);
//    }

//    public bool IsActive()
//    {
//        return gameObject.activeSelf;
//    }
//}
