using UnityEngine;
using GameUtility;

public class BaseAttackState : BasicState, ITargetExistsState
{
    private float duration;
    private float range = 1.5f;

    public BaseAttackState(Player player) : base(player)
    {
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
        if(player.DistanceToUnit(targetUnit) > player.GetBaseAttackStrategy().GetRange())
        {
            player.SetState(player.GetChaseTargetState(targetUnit));
            player.GetState().Begin();
            
        }
        else
        {
            player.SetState(this);
            player.GetState().Begin();
            
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
        if (player.DistanceToUnit(targetUnit) > player.GetBaseAttackStrategy().GetRange())
            return false;
        else
            return true;
    }

    public void OnTargetDead()
    {
        player.SetState(player.GetIdleState());
        player.GetState().Begin();
    }

    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }
}
