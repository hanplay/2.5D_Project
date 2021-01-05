using UnityEngine;

public abstract class State 
{
    protected Unit owner;
    protected Animator animator;
    protected StateSystem stateSystem;
    protected SkillSystem skillSystem; // Unit에 따라서 null 값일 수 있음


    private bool isEnded;
    private bool isBegun;

    public State(Unit owner, StateSystem stateSystem)
    {
        this.owner = owner;
        animator = owner.transform.Find("model").GetComponent<Animator>();
        this.stateSystem = stateSystem;
    }

    //호출하지 말것 StateSystem.PushState()을 통해서 호출
    public virtual void Begin()
    {
        Debug.Log(owner.ToString() + "Begin State: " + ToString());
        isEnded = false;
        isBegun = true;
    }

    //호출하지 말것 StateSystem.PopState()을 통해서 호출
    public virtual void End()
    {
        isEnded = true;
        isBegun = false;
    }

    public abstract void Tick(float deltaTime);

    public bool IsEnded()
    {
        return isEnded;
    }

    public bool IsBegun()
    {
        return isBegun;
    }

    protected void SetTargetable(bool value)
    {
        owner.GetComponent<Rigidbody>().isKinematic = !value;
        owner.GetComponent<Collider>().enabled = value;
    }
        

}
