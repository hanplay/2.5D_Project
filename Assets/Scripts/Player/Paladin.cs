using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : Player
{
	protected override void Awake()
	{
		base.Awake();
		attackSystem.Init(new InstantAttackStrategy(this, new CommonDamageStrategy()), 2f);
		targetingStrategy = new TargetingStrategy<Enemy>();
	}

	private void Start()
	{
		skillSystem.SetSkill(0, GameAssets.Instance.CreateSkill(this, SkillType.DivineChargeBuff));
		skillSystem.SetSkill(1, GameAssets.Instance.CreateSkill(this, SkillType.TeleportBuff));
	}


	protected override void Update()
	{
		base.Update();
	}
}
