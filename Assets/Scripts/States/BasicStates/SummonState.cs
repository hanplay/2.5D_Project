using UnityEngine;

public class SummonState : State
{
    private float duration;
    private float lagTime;
    public SummonState(Unit owner, StateSystem stateSystem, float duration, int StateType) : base(owner, stateSystem, StateType)
    {
        this.duration = duration;
    }

    public override void Begin()
    {
        base.Begin();
        animator.Play("Idle");
        owner.GetComponent<BasicFXVisualizer>().Summon(new Color(2.3f, 0.7f, 0.2f), duration);
        SetTargetable(false);

    }

    public override void End()
    {
        base.End();       
        SetTargetable(true);
    }

    public override void Tick(float deltaTime)
    {
        lagTime += Time.deltaTime;
        if(lagTime > duration)
        {
            stateSystem.PopState();
        }
    }
}
