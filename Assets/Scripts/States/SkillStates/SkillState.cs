using GameUtility;
using System;
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
    private float range;


    public SkillState(Player player, int stateType) : base(player, stateType) { }
    //public SkillState(Player player, SkillDatum skillDatum) :base(player, )
    //{

    //}

    public override void Accept(State state)
    {
        throw new NotImplementedException();
    }

    public override bool CanAccept(State state)
    {
        if(state.HasProperty(StateType.Skill & StateType.CanCancel))
        {
            if(false == HasProperty(StateType.CannotBeCanceled))
                return CanBegin();
        }
        return false;
            
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;
        if (null != GetNextState())
        {
            if (GetNextState().CanAccept(this))
            {
                GetNextState().Accept(this);
            }
        }

        if (IsEnded())
        {
            End();
        }
    }


    protected override void End()
    {
        duration = 0f;
        SetNextState(null);
        unit.SetCurrentState(unit.ProperBasicState());
        unit.GetCurrentState().Begin();
    }

    protected override bool IsEnded()
    {
        if (duration > 0)
            return false;
        else
        {
            return true;
        }
    }
}
