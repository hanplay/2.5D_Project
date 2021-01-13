using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Player
{
	protected override void Awake()
	{
		base.Awake();
		statsSystem.Init(5, 1);
		moveSystem.Init(new StraightMoveStrategy(this), 4f);
		healthPointsSystem.Init(300);
		targetingStrategy = new TargetingStrategy<Enemy>();
	}

	protected void Start()
	{
		ProjectileContainer projectileContainer = transform.Find("ProjectileContainer").GetComponent<ProjectileContainer>();		
		attackStrategy = new ProjectileAttackStrategy(this, new CommonDamageStrategy(), 7f, projectileContainer);
		skillSystem.SetSkill(0, GameAssets.Instance.CreateSkill(this, SkillType.IceNova));
		skillSystem.SetSkill(1, GameAssets.Instance.CreateSkill(this, SkillType.TeleportBuff));
	}



}
