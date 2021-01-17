using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command 
{
    protected Unit owner;
    public abstract void Execute();
    public void SetOwner(Unit owner)
    {
        this.owner = owner;
    }
}
