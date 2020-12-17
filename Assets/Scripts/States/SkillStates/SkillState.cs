using UnityEngine;

public abstract class SkillState : State
{
    protected Skill skill;
    protected float duration;
    protected float lagTime;
    protected bool isEnd;



    public SkillState(Unit player, StateSystem stateSystem, Skill skill) : base(player, stateSystem, Skill) 
    {
        this.owner = player;
        this.skill = skill;
    }
    public override void Begin()
    {
        skill.StartCooldownTime();

    }
    public override void Tick(float deltaTime)
    {       
        if (true == isEnd)
            return;
        lagTime += deltaTime;
        if (lagTime >= duration)
        {
            End();
        }
    }

    public override void End()
    {
        isEnd = true;
    }

    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetedUnit = targetUnit;
    }

    public virtual void Initialize()
    {
        isEnd = false;
        lagTime = 0;
    }

}
