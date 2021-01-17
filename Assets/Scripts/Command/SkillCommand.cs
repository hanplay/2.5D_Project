using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand : Command
{
    private Skill skill;
    public void Execute(Unit owner)
    {
        SetOwner(owner);
        Execute();
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

    public override void Execute()
    {
        if (skill.IsCoolDownTime())
            return;
        BasicState basicState = owner.GetStateSystem().GetCurrentState() as BasicState;
        if (null == basicState)
            return;
        if(skill.IsTargetSkill())
        {
            ITargetingBasicState targetingBasicState = basicState as ITargetingBasicState;
            targetingBasicState?.ActivateTargetingSkill(skill);
        }
        else
        {
            basicState.ActivateSkill(skill);
        }               
    }
}
