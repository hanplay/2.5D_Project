using UnityEngine;

public abstract class State 
{
    protected Unit unit;
    protected Animator animator;
    private State nextState = null;
    private int stateType;
    

    public State(Unit unit, int stateType)
    {
        this.unit = unit;
        animator = unit.transform.Find("model").GetComponent<Animator>();
        this.stateType = stateType;
    }

    public abstract void Tick(float deltaTime);
    
    /*
        만약에 State가 stateType에 해당하는 속성을 모두 가지고 있으면 True를
        반환
        ex) State : 010110,  parameter stateType : 010010 -> true
            State : 001001,  parameter stateType : 010001 -> false
    */
    public bool HaveProperty(int stateType)
    {
        int i = (this.stateType & (~stateType)) | (this.stateType | (~stateType));
        if (int.MaxValue == i)
        {
            return true;
        }
        else
            return false;
    }

    public void SetNextState(State nextState)
    {
        this.nextState = nextState;
    }
    public State GetNextState()
    {
        return nextState;
    }

    public abstract bool CanTrigger(State state);
    public abstract void Trigger(State state);
    public abstract bool CanBegin();
    public abstract void Begin();
    protected abstract bool IsEnded();
    protected abstract void End();
    
    
    public int GetStateType()
    {
        return stateType;
    }
}
