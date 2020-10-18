using UnityEngine;

public abstract class SkillState : State
{
    protected Skill skill;
    protected float duration;
    protected bool isEnd;

    public SkillState(Player player, Skill skill) : base(player) 
    {
        this.player = player;
    }

    public override void Begin()
    {
        Debug.Log("Skill Begin");
        skill.StartCooldownTime();
    }

    public bool IsEnd()
    {
        return isEnd;
    }


    public void SetTargetUnit(Unit targetUnit)
    {
        this.targetUnit = targetUnit;
    }


}
