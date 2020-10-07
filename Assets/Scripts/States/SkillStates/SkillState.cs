using UnityEngine;
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
    [SerializeField]
    protected float duration;
    [SerializeField]
    protected float cooldownTime;
    protected float lagTime;



    public SkillState(Player player, int stateType) : base(player, stateType) 
    {
        this.player = player;
    }
    //public SkillState(Player player, SkillDatum skillDatum) :base(player, )
    //{

    //}
    public override void Begin()
    {
        for (int i = 0; i < player.GetSkillCount(); i++)
        {
            if(null != player.GetSkill(i))
            {


            }
        }
    }

    public override void Accept(State state)
    {
     
        if(HasProperty(StateType.TargetExist))
        {
            SetNextState(null);
            State chainState = player.GetChaseTargetState();
            chainState.SetNextState(this);
            player.SetCurrentState(chainState);
            player.GetCurrentState().Begin();
        
        }
        else
        {
            SetNextState(null);
            player.SetCurrentState(this);
            player.GetCurrentState().Begin();
        }
        
    }

    public override bool CanAccept(State state)
    {
        if(state.HasProperty(StateType.Skill | StateType.CanCancel))
        {
            if(false == HasProperty(StateType.CannotBeCanceled))
                return CanBegin();
        }
        else if(false == state.HasProperty(StateType.Skill))
        {
            return CanBegin();
        }
        return false;          
    }

    public override void Tick(float deltaTime)
    {
        lagTime += deltaTime;
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
        lagTime = 0f;
        SetNextState(null);
        player.SetCurrentState(player.ProperBasicState());
        player.GetCurrentState().Begin();
    }

    protected override bool IsEnded()
    {
        if (lagTime < cooldownTime)
            return false;
        else
        {
            return true;
        }
    }

    public float GetRemainedProportion()
    {
        return (cooldownTime - lagTime) / cooldownTime;
    }

}
