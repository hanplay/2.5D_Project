public class Bishop : Player
{
	protected override void Awake()
	{
		base.Awake();
		attackSystem.Init(new InstantAttackStrategy(this, new HealStrategy(GameAssets.Instance.healEffect)), 4f);
		targetingStrategy = new TargetingStrategy<Player>();
	}

	protected void Start()
	{
		skillSystem.SetSkill(0, GameAssets.Instance.CreateSkill(this, SkillType.Pray));
		skillSystem.SetSkill(1, GameAssets.Instance.CreateSkill(this, SkillType.TeleportBuff));
		//skillList[1] = GameAssets.Instance.CreateSkill(this, SkillType.TestBuff);
		//skillList[2] = GameAssets.Instance.CreateSkill(this, SkillType.HasteBuff);
		//skillList[3] = GameAssets.Instance.CreateSkill(this, SkillType.Charge);
	}

	protected override void Update()
	{
		base.Update();
	}

}
