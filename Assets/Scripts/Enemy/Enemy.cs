using UnityEngine;
using System;
using System.Collections.Generic;

public class Enemy : Unit
{
    protected override void Awake()
	{
		base.Awake();
		statsSystem.Init(4, 0);
		healthPointsSystem.Init(50);
		stateSystem.PushState(new SummonState(this, 3f));
		moveSystem.Init(new StraightMoveStrategy(this), 3f);
		attackStrategy =  new InstantAttackStrategy(this, new CommonDamageStrategy(), 2f);
		targetingStrategy = new TargetingStrategy<Player>();
	}
}