using UnityEngine;

public abstract class SkillState : State
{
    protected Skill skill;
    protected float duration;
    protected float lagTime;
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
    public override void TickAccept(float deltaTime, Command command)
    {
        command.Visit(this);
        if (true == isEnd)
            return;
        lagTime += deltaTime;
        if (lagTime >= duration)
        {
            End();
        }
    }

    protected virtual void End()
    {
        isEnd = true;
    }

    public bool IsEnd()
    {
        return isEnd;
    }


    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }

    public virtual void Initialize()
    {
        isEnd = false;
        lagTime = 0;
    }

}
