using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{

    private float duration;
    private float lagTime;
    public StunState(Unit owner, float duration) : base(owner, owner.GetStateSystem())
    {

        this.duration = duration;
    }

    public StunState(Unit owner, StateSystem stateSystem, float duration) : base(owner, stateSystem)
    {        
        this.duration = duration;
    }

    public override void Begin()
    {
        base.Begin();
        animator.Play("Idle");
    }
    public override void Tick(float deltaTime)
    {
        lagTime += deltaTime;
        if(lagTime > duration)
        {
            stateSystem.PopState();
            return;
        }        
    }

    public override void End()
    {
        base.End();
    }

}
