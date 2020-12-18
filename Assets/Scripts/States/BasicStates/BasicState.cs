using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicState : State, IMoveableState
{
    public event StateSystem.SkillEventHandler OnSkill;

    public BasicState(Unit owner, StateSystem stateSystem) : base(owner, stateSystem, Basic) { }

    public abstract void ChaseTarget(Unit targetUnit);
    public abstract void MoveTo(Vector3 destination);
    
    public void ActivateSkill(Skill skill)
    {
        OnSkill.Invoke(this, skill);
    }
}
