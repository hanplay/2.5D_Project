using UnityEngine;

public abstract class BasicState : State
{
    protected BasicState(Unit unit) : base(unit) { }


    public override void Accept(State state)
    {
        state.Visit(this);
    }
    public override bool CanAccept(State state)
    {
        return state.CanVisit(this);
    }


    public override bool CanVisit(BasicState basicState)
    {
        return basicState.CanBegin();
    }

    public override bool CanVisit(SkillState skillState)
    {
        return skillState.CanBegin();
    }




    public override void Visit(BasicState basicState)
    {
        unit.SetState(basicState);
        basicState.Begin();
        SetNextState(null);
    }

    public override void Visit(SkillState skillState)
    {
        unit.SetState(skillState);
        skillState.Begin();
        SetNextState(null);
    }
    public override void OnTargetIsDead()
    {
        if(isStateTargetingUnit)
        {
            unit.SetNextState(unit.GetIdleState());
        }
    }

    public override bool CanCancel()
    {
        return true;
    }
}

