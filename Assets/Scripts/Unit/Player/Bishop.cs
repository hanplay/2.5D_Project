public class Bishop : Player
{
	protected override void Awake()
	{
		base.Awake();
		statsSystem.Init(5, 1);
		moveSystem.Init(new StraightMoveStrategy(this), 4f);
		healthPointsSystem.Init(300);
		targetingStrategy = new TargetingStrategy<Player>();
	}

	protected void Start()
	{
		attackStrategy =  new InstantAttackStrategy(this, new HealStrategy(GameAssets.Instance.healEffect), 4f);
		skillSystem.SetSkill(0, GameAssets.Instance.CreateSkill(this, SkillType.Cure));
		skillSystem.SetSkill(1, GameAssets.Instance.CreateSkill(this, SkillType.Pray));
			
	}

}
