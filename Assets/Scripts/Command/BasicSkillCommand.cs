public class BasicSkillCommand : SkillCommand
{
    public BasicSkillCommand(Player player, Skill skill) : base(player, skill) { }

    public override void Visit(IdleState idleState)
    {
        idleState.ChageToSkillState(skillState);
    }

    public override void Visit(MoveState moveState)
    {
        moveState.ChageToSkillState(skillState);
    }

    public override void Visit(AttackState attackState)
    {
        attackState.ChageToSkillState(skillState);
    }

    public override void Visit(SkillState skillState)
    {
        return;
    }
}
