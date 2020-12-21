using UnityEngine;

public class Knight : Player
{
	//private Transform 
	protected override void Awake()
	{
		base.Awake();		
		attackSystem.Init(new InstantAttackStrategy(this, new CommonDamageStrategy()), 2f);
	}
	
	private void Start()
    {
		skillSystem.SetSkill(0, GameAssets.Instance.CreateSkill(this, SkillType.Dive));
		skillSystem.SetSkill(1, GameAssets.Instance.CreateSkill(this, SkillType.DeadlyPoisonBuff));
		skillSystem.SetSkill(2, GameAssets.Instance.CreateSkill(this, SkillType.HasteBuff));
		skillSystem.SetSkill(3, GameAssets.Instance.CreateSkill(this, SkillType.Charge));		
	}


	protected override void Update()
    {
		base.Update();

	}

	public override bool IsTargetable(Unit unit)
	{
		if (null == unit)
			return false;

		if(null != unit.GetComponent<Enemy>())
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}

