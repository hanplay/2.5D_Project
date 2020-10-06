using GameUtility;
using UnityEngine;

public abstract class BasicState : State
{
    protected BasicState(Unit unit, int stateType) : base(unit, stateType) { }

    public override void Accept(State state)
    {
        state.SetNextState(null);
        unit.SetCurrentState(this);
        unit.GetCurrentState().Begin();
    }

    public override bool CanAccept(State state)
    {
        return state.CanBegin(); 
    }

    public override void Tick(float deltaTime)
    {
        
        if (null != GetNextState())
        {
            if (GetNextState().CanAccept(this))
            {
                GetNextState().Accept(this);
            }
        }

        if (IsEnded())
        {
            End();
        }
    }


}

