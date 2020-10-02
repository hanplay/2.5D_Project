using UnityEngine;

public abstract class State 
{
    protected Unit unit;
    protected Unit targetUnit;
    protected Animator animator;
    private State nextState = null;

    public State(Unit unit)
    {
        this.unit = unit;
        animator = unit.transform.Find("model").GetComponent<Animator>();
    }

    public abstract void Tick(float deltaTime);

    #region Visitor Pattern

    public abstract bool CanVisit(BasicState basicState);
    public abstract bool CanVisit(SkillState skillState);
    public abstract void Visit(BasicState basicState);
    public abstract void Visit(SkillState skillState);
    public abstract bool CanAccept(State state);
    public abstract void Accept(State state);

    #endregion

    public abstract void Begin();
    protected abstract bool IsEnded();
    protected abstract void End();
    
    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
    public State GetNextState()
    {
        return nextState;
    }

}
