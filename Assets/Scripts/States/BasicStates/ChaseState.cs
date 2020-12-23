using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BasicState, IMoveableState, ITargetingBasicState
{
    public event StateSystem.MoveEventHandler OnMove;
    public event StateSystem.TargetUnitEventHandler OnAttack;
    public event StateSystem.TargetSkillEventHandler OnTargetingSkill;

    private MoveSystem moveSystem;
    private AttackSystem attackSystem;
    private Skill targetingSkill;
    

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
       
        if(null != targetingSkill)
        {
            if(owner.DistanceToUnit(targetedUnit) <= targetingSkill.GetRange())
            {                
                OnTargetingSkill.Invoke(this, targetingSkill, targetedUnit);
            }
        }

        if(true == attackSystem.InRange(targetedUnit))
        {
            OnAttack.Invoke(this, targetedUnit);
        }
        else
        {
            owner.FlipToTarget(targetedUnit);
            moveSystem.GetUsingMoveStrategy().ChaseTarget(targetedUnit);
        }
    }

    public override void ChaseTarget(Unit targetedUnit)
    {
        this.targetedUnit = targetedUnit;
    }

    public override void MoveTo(Vector3 destination)
    {
        OnMove.Invoke(this, destination);
    }

    public void ActivateTargetingSkill(Skill targetingSkill)
    {
        if (owner.DistanceToUnit(targetedUnit) <= targetingSkill.GetRange())
        {
            OnTargetingSkill.Invoke(this, targetingSkill, targetedUnit);
        }
        else
        {
            ReserveTargetingSkill(targetingSkill);
        }
    }

    public void ReserveTargetingSkill(Skill targetingSkill)
    {
        this.targetingSkill = targetingSkill;        
        //ToDo
    }

    public override void End()
    {
        base.End();
        targetingSkill = null;
    }
}
