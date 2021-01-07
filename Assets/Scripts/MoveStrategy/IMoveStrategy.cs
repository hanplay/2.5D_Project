using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveStrategy 
{
    void MoveTo(Vector3 destination);
    void ChaseTarget(Unit targetUnit);
    void ReverseChaseTarget(Unit targetedUnit);
}
