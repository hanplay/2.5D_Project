using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillSystem 
{
    private Player player;
    public const int SkillCount = 4;
    private Skill reservedTargetingSkill;

    private Skill[] skillList = new Skill[SkillCount];
    public Action[] SkillAction = new Action[SkillCount];
    public SkillSystem(Player player)
    {
        this.player = player;
    }

    public void Tick(float deltaTime)
    {
        for (int i = 0; i < skillList.Length; i++)
        {
            skillList[i]?.Tick(deltaTime);
        }
    }



    public bool IsTargetingSkillReserved()
    {
        if (null == reservedTargetingSkill)
            return false;
        else
            return true;
    }

    public void SetSkill(int i, Skill skill)
    {
        if (SkillCount <= i)
            return;
        skillList[i] = skill;
    }

    public Skill GetSkill(int i)
    {
        return skillList[i];
    }

    public bool InRange(Unit targetedUnit)
    {
        if (player.DistanceToUnit(targetedUnit) <= reservedTargetingSkill.GetRange())
        {
            return true;
        }
        else
            return false;
    }
}
