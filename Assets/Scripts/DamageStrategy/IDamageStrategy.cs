using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageStrategy 
{
    void Do(Unit targetUnit, int damage);
}
