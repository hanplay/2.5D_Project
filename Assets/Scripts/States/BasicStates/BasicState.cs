using GameUtility;
using UnityEngine;

public abstract class BasicState : State
{
    protected BasicState(Unit unit, int stateType) : base(unit, stateType) { }

    public override void Tick(float deltaTime)
    {
        {
            if (null != GetNextState())
            {
                if (CanTrigger(GetNextState()))
                {
                    Trigger(GetNextState());
                }
            }

            if (IsEnded())
            {
                End();
            }

        }
    }


    public override bool CanTrigger(State state)
    {
        if (state.HaveProperty(StateType.TargetExist & StateType.Skill))
        {
            return HaveProperty(StateType.TargetExist);
        }
        return true;
    }

    public override void Trigger(State state)
    {
        if(state.HaveProperty(StateType.TargetExist & StateType.Skill))
        {
            SetNextState(null);
            State chainState = unit.GetChaseTargetState();
            chainState.SetNextState(state);
            unit.SetState(chainState);
            unit.GetState().Begin();
        }
        else
        {
            SetNextState(null);
            unit.SetState(state);
            unit.GetState().Begin();
        }
    }
}

