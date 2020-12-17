using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackingState 
{
    void Attack(Unit targetUnit);
    float GetRange();
}
