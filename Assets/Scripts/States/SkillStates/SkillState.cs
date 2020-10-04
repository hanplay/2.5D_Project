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


    public SkillState(Player player) : base(player) { }


    public override void Accept(State state)
    {
        state.Visit(this);
    }



    public override bool CanAccept(State state)
    {
        return state.CanVisit(this);
    }



    public override bool CanVisit(BasicState basicState)
    {
        return false;
    }

    public override bool CanVisit(SkillState skillState)
    {
        return skillState.CanBegin() && canCancel;
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Visit(BasicState basicState)
    {
        throw new NotImplementedException();
    }

    public override void Visit(SkillState skillState)
    {
        throw new NotImplementedException();
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
