using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : ICommand
{
    private Skill skill;
    public void Execute(Unit unit)
    {
        if (skill.IsCoolDownTime())
            return;
        BasicState basicState = unit.GetStateSystem().GetCurrentState() as BasicState;
        if(skill.IsTargetSkill())
        {
            ITargetingBasicState targetingBasicState = basicState as ITargetingBasicState;
            targetingBasicState.ActivateTargetingSkill(skill);
        }
        else
        {
            basicState.ActivateSkill(skill);
        }
        
    }

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
    }

    public void Execute(Unit unit, Skill skill)
    {
        SetSkill(skill);
        Execute(unit);
    }
}
