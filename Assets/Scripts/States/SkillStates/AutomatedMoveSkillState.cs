﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUtility;

public abstract class AutomatedMoveSkillState : SkillState
{
    public AutomatedMoveSkillState(Player player) : base(player, StateType.Skill | StateType.CanCancel | StateType.CannotBeCanceled)
    {
    }
    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
    }



    

}