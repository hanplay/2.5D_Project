using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum SkillType
{
    Heal,
    Meteor,
    Aggro,
    Dive,
    Charge,
    RapidFire,
    PoisonedArrow,
    IceAge,
    Buff,
    COUNT,
}


public class SkillFactory 
{
    SkillData skillData;
    public SkillFactory(SkillData skillData)
    {
        this.skillData = skillData;
    }


    public Skill CreateSkill(Player player, SkillType skillType)
    {
        Skill skill = new Skill(player);
        switch (skillType)
        {
        case SkillType.Dive:
            skill.SetCanCancel(true);
            skill.SetCooldownTime(5f);
            skill.SetIsTargetSkill(true);
            skill.SetSkillSprite(skillData.DiveSkillSprite);
            skill.SetSkillState(new DiveSkillState(player, skill, skillData.Smoke));
            return skill;
        default:
            Assert.IsTrue(true);
            Debug.LogError("Skill Type does not Exist!!!!");
            return skill;
        }
    }
}
