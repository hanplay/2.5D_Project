public class BasicSkillCommand : SkillCommand
{
    public BasicSkillCommand(Player player, Skill skill) : base(player, skill) { }



    public override void Visit(SkillState skillState)
    {
        //if(skillState.IsEnd())
        //{
        //    skillState
        //}
    }

    public override void Visit(BasicState basicState)
    {
        SkillState skillState = skill.GetSkillState();
        skillState.SetTargetUnit(basicState.GetTargetUnit());
        basicState.ChageToSkillState(skill.GetSkillState());
    }
}
