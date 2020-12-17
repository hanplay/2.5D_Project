using UnityEngine;

public class Knight : Player
{
	//private Transform 
	protected override void Awake()
	{
		//attackStrategy = new InstantAttackStrategy(this, new CommonDamageStrategy());
		attackSystem = new AttackSystem(this, new InstantAttackStrategy(this, new CommonDamageStrategy()), 2f);
		base.Awake();
	}
	
	protected void Start()
    {
		skillList[0] = GameAssets.Instance.CreateSkill(this, SkillType.Dive);
		skillList[1] = GameAssets.Instance.CreateSkill(this, SkillType.DeadlyPoisonBuff);
		skillList[2] = GameAssets.Instance.CreateSkill(this, SkillType.HasteBuff);
		skillList[3] = GameAssets.Instance.CreateSkill(this, SkillType.Charge);
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

