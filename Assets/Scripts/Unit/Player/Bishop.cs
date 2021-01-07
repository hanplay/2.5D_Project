public class Bishop : Player
{
	protected override void Awake()
	{
		base.Awake();
		attackStrategy =  new InstantAttackStrategy(this, new HealStrategy(GameAssets.Instance.healEffect), 4f);
		targetingStrategy = new TargetingStrategy<Player>();
	}

	protected void Start()
	{
		skillSystem.SetSkill(0, GameAssets.Instance.CreateSkill(this, SkillType.Pray));
		skillSystem.SetSkill(1, GameAssets.Instance.CreateSkill(this, SkillType.TeleportBuff));		
	}

	protected override void Update()
	{
		base.Update();
	}

}
