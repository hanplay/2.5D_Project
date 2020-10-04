using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetExistDecorator : State
{
    private State state;
    private Unit targetUnit;



    public TargetExistDecorator(State state) : base(state.GetUnit())
    {
        this.state = state;
    }
    public override void Tick(float deltaTime)
    {
        state.Tick(deltaTime);
    }

    public override void End()
    {
        //basic state
    }

    public override bool IsEnded()
    {
        return state.IsEnded();
    }

    public override void SetNextState(State nextState)
    {
        state.SetNextState(nextState);
    }

    public override State GetNextState()
    {
        return state.GetNextState();
    }
}
