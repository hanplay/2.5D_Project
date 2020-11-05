using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy 
{
    void Attack(Unit targetUnit);
    void AnimationEventOccur();
    void SetDamageStrategy(DamageStrategy damageStrategy);
    DamageStrategy GetDamageStrategy();
    void SetRange(float range);
    float GetRange();
}

