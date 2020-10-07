using UnityEngine;

public abstract class State 
{
    protected Player player;
    protected Animator animator;
    private State nextState = null;
    private int stateType;
    

    public State(Player player, int stateType)
    {
        this.player = player;
        animator = player.transform.Find("model").GetComponent<Animator>();
        this.stateType = stateType;
    }

    public abstract void Tick(float deltaTime);
    
    /*
        만약에 State가 stateType에 해당하는 속성을 모두 가지고 있으면 True를
        반환
        ex) State : 010110,  parameter stateType : 010010 -> true
            State : 001001,  parameter stateType : 010001 -> false
    */
    public bool HasProperty(int stateType)
    {
        /*
            0 oper 0 -> 1
            0 oper 1 -> 0
            1 oper 0 -> 1
            1 oper 1 -> 1                            
        */
        int bitMask = (this.stateType & (~stateType)) | (this.stateType | (~stateType));
        if (int.MaxValue == bitMask)
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

    public abstract bool CanAccept(State state);
    public abstract void Accept(State state);
    public abstract bool CanBegin();
    public abstract void Begin();
    protected abstract bool IsEnded();
    protected abstract void End();
    
    
    public int GetStateType()
    {
        return stateType;
    }
}
