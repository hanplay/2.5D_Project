﻿public abstract class SkillCommand : Command
{
    protected Skill skill;

    public SkillCommand(Player player, Skill skill) : base(player)
    {
        this.skill = skill;
    }

    public override void Visit(DieState dieState)
    {
        return;
    }
}
