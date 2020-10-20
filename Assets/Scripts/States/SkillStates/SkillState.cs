using UnityEngine;

public abstract class SkillState : State
{
    protected Skill skill;
    protected float duration;
    protected bool isEnd;



    public SkillState(Player player, Skill skill) : base(player) 
    {
        this.player = player;
        this.skill = skill;
    }

    public override void Begin()
    {
        skill.StartCooldownTime();
        if(null == targetUnit)
        {
            player.SetCommand(new NullCommand(player));
        }
        else
        {
            player.SetCommand(new AttackCommand(player, targetUnit));
        }
    }

    public bool IsEnd()
    {
        return isEnd;
    }


    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

    public abstract void Initialize();

}
