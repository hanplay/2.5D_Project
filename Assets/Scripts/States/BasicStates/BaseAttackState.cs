using UnityEngine;
using GameUtility;

public class BaseAttackState : BasicState
{
    private float duration;
    private float range = 1.5f;

    public BaseAttackState(Unit unit) : base(unit, StateType.Basic & StateType.TargetExist)
    {
    }
    public override bool CanBegin()
    {
        if (unit.ToTargetUnitDistance() > range)
        {
            return false;
        }
        else
            return true;
    }

    public override void Begin()
    {
        unit.BaseAttackAction = Work;
        animator.Play("Attack");
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
        if(unit.ToTargetUnitDistance() > range)
        {
            unit.SetCurrentState(unit.GetChaseTargetState());
            unit.GetCurrentState().Begin();
            SetNextState(null);
        }
        else
        {
            unit.SetCurrentState(this);
            unit.GetCurrentState().Begin();
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

    public bool IsTargetUnitInRange()
    {
        if (unit.ToTargetUnitDistance() > range)
            return false;
        else
            return true;
    }

}
