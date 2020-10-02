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



    public SkillState(Player player) : base(player)
    {
    }


}
