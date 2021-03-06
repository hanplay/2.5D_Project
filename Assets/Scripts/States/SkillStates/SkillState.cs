﻿using UnityEngine;

public abstract class SkillState : State
{
    protected Skill skill;
    protected float duration;
    protected float lagTime;
    protected bool isEnd;



    public SkillState(Unit player, StateSystem stateSystem, Skill skill) : base(player, stateSystem) 
    {
        this.owner = player;
        this.skill = skill;
    }
    public override void Begin()
    {
        base.Begin();
        skill.StartCooldownTime();

    }
    public override void Tick(float deltaTime)
    {       
        if (true == isEnd)
            return;
        lagTime += deltaTime;
        if (lagTime >= duration)
        {
            stateSystem.PopState();
        }
    }

    public override void End()
    {
        base.End();
    }

    public virtual void Initialize()
    {
        isEnd = false;
        lagTime = 0;
    }

}
