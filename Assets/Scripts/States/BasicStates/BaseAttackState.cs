using UnityEngine;

public class BaseAttackState : BasicState, ITargetExistState
{
    private float duration;
    private float range = 1.5f;

    public BaseAttackState(Unit unit) : base(unit)
    {
    }

    public override void Begin()
    {
        unit.BaseAttackAction = Work;
        animator.Play("Attack", -1);
        duration = animator.GetCurrentAnimatorStateInfo(0).length;
    }


    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        if (0 >= duration)
        {
            End();
            duration = 0f;
        }
        duration -= deltaTime * animator.speed;
    }

    protected override void End()
    {
        if(unit.DistanceToUnit(targetUnit) < range)
        {
            unit.SetState(this);
            unit.GetState().Begin();
            SetNextState(null);
        }
        else
        {
            unit.SetState(unit.GetChaseTargetState(targetUnit));
            unit.GetState().Begin();
            SetNextState(null);
        }
    }

    public void Work()
    {
        Debug.Log("Damage to targetUnit");
    }


    protected override bool IsEnded()
    {
        if(0 >= duration)
        {
            return true;
        }
        return false;
    }

    public float GetRange()
    {
        return range;
    }

    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

    public void OnTargetIsDead()
    {
        targetUnit = null;
        SetNextState(unit.GetIdleState());
    }
}
