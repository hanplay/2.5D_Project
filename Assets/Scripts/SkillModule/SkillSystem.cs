using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem 
{
    private Player player;

    private Skill reservedTargetingSkill;

    public SkillSystem(Player player)
    {
        this.player = player;
    }

    public bool IsTargetingSkillReserved()
    {
        if (null == reservedTargetingSkill)
            return false;
        else
            return true;
    }
}
