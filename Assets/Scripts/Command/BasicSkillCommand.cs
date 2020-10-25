public class BasicSkillCommand : SkillCommand
{
    public BasicSkillCommand(Player player, Skill skill) : base(player, skill) { }

    public override void Visit(IdleState idleState)
    {
        idleState.ChageToSkillState(skill.GetSkillState());
    }

    public override void Visit(MoveState moveState)
    {
        moveState.ChageToSkillState(skill.GetSkillState());
    }
    public override void Visit(ChaseState chaseState)
    {
        chaseState.ChageToSkillState(skill.GetSkillState());
    }

    public override void Visit(AttackState attackState)
    {       
        attackState.ChageToSkillState(skill.GetSkillState());
    }

    public override void Visit(SkillState skillState)
    {
        //if(skillState.IsEnd())
        //{
        //    skillState
        //}
    }

}
