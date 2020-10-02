public class BuffSkill : Skill
{
	private BuffDatum buffDatum;
	public BuffSkill(SkillDatum skillDatum, BuffDatum buffDatum, Player player) : base(skillDatum, player) 
	{
		this.buffDatum = buffDatum;
	}

	public override bool CanExecute()
	{
		throw new System.NotImplementedException();
	}
}
