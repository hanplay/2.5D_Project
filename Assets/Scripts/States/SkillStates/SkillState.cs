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
    protected float duration;

    public SkillState(Player player, SkillDatum skillDatum) : base(player)
    {

    }

    public SkillState(Player player) : base(player) 
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

    //public override void Tick(float deltaTime)
    //{
    //    if (IsEnded())
    //    {
    //        End();
    //    }
    //}

    //protected override bool IsEnded()
    //{
    //    if (lagTime < cooldownTime)
    //        return false;
    //    else
    //    {
    //        return true;
    //    }
    //}

}
