using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BasicState, IMoveableState, ITargetingBasicState
{
    public event StateSystem.MoveEventHandler OnMove;
    public event StateSystem.TargetUnitEventHandler OnAttack;
    public event StateSystem.TargetSkillEventHandler OnTargetingSkill;

    private MoveSystem moveSystem;
    private TargetedUnitHandler targetedUnitHandler;
    private Skill targetingSkill;
    

    public ChaseState(Unit owner, StateSystem stateSystem) : base(owner, stateSystem) 
    {
        moveSystem = owner.GetMoveSystem();
        targetedUnitHandler = owner.GetTargetedUnitHandler();
    }

    public override void Begin()
    {
        base.Begin();
        animator.Play("Run");
    }


    public override void Tick(float deltaTime)
    {
        if(false == targetedUnitHandler.TryGetTargetedUnit(out Unit targetedUnit))
        {
            owner.GetStateSystem().PopState();
            return;
        }
       
        if(null != targetingSkill)
        {
            if(owner.DistanceToUnit(targetedUnit) <= targetingSkill.GetRange())
            {                
                OnTargetingSkill.Invoke(this, targetingSkill);
            }
        }

        if(true == targetedUnitHandler.TargetInAttackRange())
        {
            OnAttack.Invoke(this);
        }
        else
        {
            owner.FlipToTarget(targetedUnit);
            moveSystem.GetUsingMoveStrategy().ChaseTarget(targetedUnit);
        }
    }

    public override void ChaseTarget(Unit targetedUnit)
    {

    }

    public override void MoveTo(Vector3 destination)
    {
        OnMove.Invoke(this, destination);
    }

    public void ActivateTargetingSkill(Skill targetingSkill)
    {
        if (true == targetedUnitHandler.TargetInSkillRange(targetingSkill))
        {

        }
        else
        {

        }
    }

    public void ReserveTargetingSkill(Skill targetingSkill)
    {
        this.targetingSkill = targetingSkill;        
        //ToDo
    }
}
