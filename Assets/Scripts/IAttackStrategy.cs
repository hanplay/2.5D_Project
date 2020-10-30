using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy 
{
    float GetRange();
    void Attack(Unit targetUnit);
    void Damage(Unit targetUnit);   
    void ActivateVisualEffect(Unit targetUnit);
}
