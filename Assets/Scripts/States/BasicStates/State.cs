using UnityEngine;

public abstract class State 
{
    protected Unit owner;
    protected Animator animator;
    protected StateSystem stateSystem;
    protected Unit targetedUnit;


    public const int Die = -1;
    public const int Idle = 0;
    public const int Basic = 1;
    public const int Skill = 2;

    public readonly int StateType;

    private bool isEnded;
    private bool isBegun;

    public State(Unit owner, StateSystem stateSystem, int StateType)
    {
        this.owner = owner;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        this.stateSystem = stateSystem;
        this.StateType = StateType;
    }

    public virtual void Begin()
    {
        Debug.Log("Begin State: " + this.ToString());
        isEnded = false;
        isBegun = true;
    }
    public virtual void End()
    {
        isEnded = true;
        isBegun = false;
    }

    public abstract void Tick(float deltaTime);
    public abstract bool IsTargetingState();

    public Unit GetTargetUnit()
    {
        return targetedUnit;
    }

    public bool IsEnded()
    {
        return isEnded;
    }

    public bool IsBegun()
    {
        return isBegun;
    }

}
