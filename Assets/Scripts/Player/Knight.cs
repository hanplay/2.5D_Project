public class Knight : Player
{
	//private Transform 
	protected override void Awake()
	{
		base.Awake();		
		attackStrategy = new InstantAttackStrategy(this, new CommonDamageStrategy(), 2f);
		targetingStrategy = new TargetingStrategy<Enemy>();
	}
	
	private void Start()
    {		
		skillSystem.SetSkill(0, GameAssets.Instance.CreateSkill(this, SkillType.DeadlyPoisonBuff));
		skillSystem.SetSkill(1, GameAssets.Instance.CreateSkill(this, SkillType.HasteBuff));
	}


	protected override void Update()
    {
		base.Update();
	}
}

