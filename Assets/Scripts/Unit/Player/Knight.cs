public class Knight : Player
{
	//private Transform 
	protected override void Awake()
	{
		base.Awake();
		statsSystem.Init(5, 1);
		moveSystem.Init(new StraightMoveStrategy(this), 4f);
		healthPointsSystem.Init(300);
		targetingStrategy = new TargetingStrategy<Enemy>();
	}
	
	private void Start()
    {		
		attackStrategy = new InstantAttackStrategy(this, new CommonDamageStrategy(), 2f);
		skillSystem.SetSkill(0, GameAssets.Instance.CreateSkill(this, SkillType.DeadlyPoisonBuff));
		skillSystem.SetSkill(1, GameAssets.Instance.CreateSkill(this, SkillType.HasteBuff));
	}

}

