using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageStrategy 
{
    public abstract void Do(Unit targetUnit, int damage);
}
