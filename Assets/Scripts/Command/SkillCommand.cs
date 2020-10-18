public abstract class SkillCommand : Command
{
    protected Skill skill;
    protected SkillState skillState;

    public SkillCommand(Player player, Skill skill) : base(player)
    {
        this.skill = skill;
    }
}
