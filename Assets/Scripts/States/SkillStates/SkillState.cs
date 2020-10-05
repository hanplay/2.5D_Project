using System;
using UnityEngine;

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

public abstract class SkillState : State
{
    protected float duration;
    private float cooldownTime;
    private bool canCancel;


    public SkillState(Player player, int stateType) : base(player, stateType) { }

    public override void Tick(float deltaTime)
    {
        
    }

    protected override void End()
    {
        throw new NotImplementedException();
    }

    protected override bool IsEnded()
    {
        throw new NotImplementedException();
    }
}
