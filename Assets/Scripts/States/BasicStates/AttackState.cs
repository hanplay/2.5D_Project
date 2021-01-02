using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BasicState, IMoveableState, ITargetingBasicState
{
    public event StateSystem.MoveEventHandler OnMove;
    public event StateSystem.TargetUnitEventHandler OnChase;
    public event StateSystem.TargetSkillEventHandler OnTargetingSkill;
    public event StateSystem.TargetSkillEventHandler OnTargetingSkillReserve;
    public AttackState(Unit owner, StateSystem stateSystem) : base(owner, stateSystem) 
    {
        targetedUnitHandler = owner.GetTargetedUnitHandler();

    }

    private TargetedUnitHandler targetedUnitHandler;

    public override void Begin()
    {
        base.Begin();        
    }

    public override void Tick(float deltaTime)
    {
        if(false == targetedUnitHandler.TryGetTargetedUnit(out Unit targetedUnit))
        {
            stateSystem.PopState();
            return;
        }
        if(true == targetedUnitHandler.TargetInAttackRange())
        {
            owner.FlipToTarget(targetedUnit);
            owner.GetAttackStrategy().Attack(targetedUnit);
        }
        else
        {
            OnChase.Invoke(this);
        }
    }

    public override void ChaseTarget(Unit targetedUnit)
    {
        OnChase.Invoke(this);
    }

    public override void MoveTo(Vector3 destination)
    {
        OnMove.Invoke(this, destination);
    }

    public void ActivateTargetingSkill(Skill targetingSkill)
    {
        if(true == targetedUnitHandler.TargetInSkillRange(targetingSkill))
        {
            OnTargetingSkill.Invoke(this, targetingSkill);
        }
        else
        {
            OnTargetingSkillReserve.Invoke(this, targetingSkill);
        }
    }

}
