using UnityEngine;

public class Knight : Player
{
	//private Transform 
	protected override void Awake()
	{
		base.Awake();
		attackStrategy = new InstantAttackStrategy(this, new CommonDamageStrategy(), 1.5f);
	}
	
	protected void Start()
    {
		skillList[0] = GameAssets.Instance.CreateSkill(this, SkillType.Dive);
		skillList[1] = GameAssets.Instance.CreateSkill(this, SkillType.DeadlyPoisonBuff);
		skillList[2] = GameAssets.Instance.CreateSkill(this, SkillType.HasteBuff);
		skillList[3] = GameAssets.Instance.CreateSkill(this, SkillType.Charge);
	}


	protected void Update()
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

