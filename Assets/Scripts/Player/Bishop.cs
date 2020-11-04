public class Bishop : Player
{
	protected override void Awake()
	{
		base.Awake();
		attackStrategy = new InstantAttackStrategy(this, new HealStrategy(), "Spell", 6f);
	}

	protected void Start()
	{
		//skillList[0] = GameAssets.Instance.CreateSkill(this, SkillType.Dive);
		//skillList[1] = GameAssets.Instance.CreateSkill(this, SkillType.TestBuff);
		//skillList[2] = GameAssets.Instance.CreateSkill(this, SkillType.HasteBuff);
		//skillList[3] = GameAssets.Instance.CreateSkill(this, SkillType.Charge);
	}

	public override bool IsTargetable(Unit unit)
	{
		if (null == unit)
			return false;

		if (null != unit.GetComponent<Player>())
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
