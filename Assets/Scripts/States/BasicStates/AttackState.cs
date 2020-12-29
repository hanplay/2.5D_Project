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
        attackSystem = owner.GetAttackSystem();
    }

    private AttackSystem attackSystem;

    public override void Begin()
    {
        base.Begin();
        Attack(targetedUnit);
    }

    public override void Tick(float deltaTime)
    {
        if(null == targetedUnit)
        {
            stateSystem.PopState();
            return;
        }
        if(true == attackSystem.InRange(targetedUnit))
        {
            owner.FlipToTarget(targetedUnit);
            attackSystem.GetAttackStrategy().Attack(targetedUnit);
        }
        else
        {
            OnChase.Invoke(this, targetedUnit);
        }
    }

    public void Attack(Unit targetedUnit)
    {
        SetTarget(targetedUnit);       
    }

    public override void ChaseTarget(Unit targetedUnit)
    {
        OnChase.Invoke(this, targetedUnit);
    }

    public override void MoveTo(Vector3 destination)
    {
        OnMove.Invoke(this, destination);
    }

    public void ActivateTargetingSkill(Skill targetingSkill)
    {
        if(owner.DistanceToUnit(targetedUnit) <= targetingSkill.GetRange())
        {
            OnTargetingSkill.Invoke(this, targetingSkill, targetedUnit);
        }
        else
        {
            OnTargetingSkillReserve.Invoke(this, targetingSkill, targetedUnit);
        }
    }

    public void SetTarget(Unit targetedUnit)
    {
        this.targetedUnit = targetedUnit;
        targetedUnit.GetHealthPointsSystem().OnDead += AttackState_OnDead;
    }

    private void AttackState_OnDead(object sender, System.EventArgs e)
    {
        targetedUnit = null;
    }

    public void ReleaseTarget()
    {
        targetedUnit.GetHealthPointsSystem().OnDead -= AttackState_OnDead;
        targetedUnit = null;
    }
}
