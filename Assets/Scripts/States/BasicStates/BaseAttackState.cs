using UnityEngine;
using GameUtility;

public class BaseAttackState : BasicState
{
    private float duration;
    private float range = 1.5f;

    public BaseAttackState(Player player) : base(player, StateType.Basic & StateType.TargetExist)
    {
    }
    public override bool CanBegin()
    {
        if (player.ToTargetUnitDistance() > range)
        {
            return false;
        }
        else
            return true;
    }

    public override void Begin()
    {
        player.BaseAttackAction = Work;
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
        if(player.ToTargetUnitDistance() > range)
        {
            player.SetCurrentState(player.GetChaseTargetState());
            player.GetCurrentState().Begin();
            SetNextState(null);
        }
        else
        {
            player.SetCurrentState(this);
            player.GetCurrentState().Begin();
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
        if (player.ToTargetUnitDistance() > range)
            return false;
        else
            return true;
    }

}
