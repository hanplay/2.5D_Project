using UnityEngine;

public abstract class BasicState : State
{
    protected BasicState(Unit unit) : base(unit)
    {
    }


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
        return true;
    }

    public override bool CanVisit(SkillState skillState)
    {
        throw new System.NotImplementedException();
    }

    public override void Tick(float deltaTime)
    {
        if(IsEnded())
        {
            End();
        }
        if(null == targetUnit)
        {

        }
        if (null == GetNextState())
            return;
        if(true == GetNextState().CanAccept(this))
        {
            GetNextState().Accept(this);
        }

      
    }

    public override void Visit(BasicState basicState)
    {
        unit.SetState(basicState);
        basicState.Begin();
        SetNextState(null);
    }

    public override void Visit(SkillState skillState)
    {
        throw new System.NotImplementedException();
    }
}

