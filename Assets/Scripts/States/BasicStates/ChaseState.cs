using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BasicState, IMoveableState
{
    public event StateSystem.MoveEventHandler OnMove;
    public event StateSystem.TargetUnitEventHandler OnAttack;
    public event StateSystem.TargetUnitEventHandler OnSkill;

    private MoveSystem moveSystem;
    private AttackSystem attackSystem;
    

    public ChaseState(Unit owner, StateSystem stateSystem) : base(owner, stateSystem) 
    {
        moveSystem = owner.GetMoveSystem();
        attackSystem = owner.GetAttackSystem();
    }

    public override void Begin()
    {
        base.Begin();
        animator.Play("Run");
    }


    public override void Tick(float deltaTime)
    {
        if(null == targetedUnit)
            owner.GetStateSystem().PopState();
       
        if(null != skillSystem)
        {
            if (false == skillSystem.IsTargetingSkillReserved())
            {
                return;
            }
            else if(skillSystem.InRange(targetedUnit))
            {
                
                return;
            }
        }
        

        if(true == attackSystem.InRange(targetedUnit))
        {
            OnAttack.Invoke(this, targetedUnit);
        }
        else
        {
            moveSystem.GetUsingMoveStrategy().MoveTo(targetedUnit.GetPosition());
        }
    }
    public override bool IsTargetingState()
    {
        return true;
    }

    public override void ChaseTarget(Unit targetedUnit)
    {
        this.targetedUnit = targetedUnit;
    }

    public override void MoveTo(Vector3 destination)
    {
        OnMove.Invoke(this, destination);
    }
}
