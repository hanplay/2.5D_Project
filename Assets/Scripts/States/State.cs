using UnityEngine;

public abstract class State 
{
    protected Unit unit;
    protected Animator animator;
    protected bool isStateTargetingUnit;
    private State nextState = null;

    public State(Unit unit)
    {
        this.unit = unit;
        animator = unit.transform.Find("model").GetComponent<Animator>();
    }

    public virtual void Tick(float deltaTime)
    {
        if(null != nextState)
        {
            if(nextState.CanAccept(this))
            {
                nextState.Accept(this);
            }
        }

        if(IsEnded())
        {
            End();
        }

    }

    #region Visitor Pattern

    public abstract bool CanVisit(BasicState basicState);
    public abstract bool CanVisit(SkillState skillState);
    public abstract void Visit(BasicState basicState);
    public abstract void Visit(SkillState skillState);
    public abstract bool CanAccept(State state);
    public abstract void Accept(State state);

    #endregion

    public abstract bool CanBegin();
    public abstract void Begin();
    protected abstract bool IsEnded();
    protected virtual void End()
    {
        if(null != nextState)
        {
            if(nextState.CanBegin())
            {
                unit.SetState(nextState);
                nextState = null;
            }
        }
    }
    
    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
    public State GetNextState()
    {
        return nextState;
    }

    public abstract void OnTargetIsDead();

}
