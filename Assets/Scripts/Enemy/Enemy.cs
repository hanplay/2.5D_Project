using UnityEngine;
using System;

public class Enemy : Unit
{
	public event EventHandler OnRest;

    protected override void Awake()
	{
		base.Awake();
		statsSystem.Init(4, 0);
		healthPointsSystem.Init(200);
		moveSystem.Init(new StraightMoveStrategy(this), 3f);
		attackSystem.Init(new InstantAttackStrategy(this, new CommonDamageStrategy()), 1.5f);
	}
	
	protected override void Update()
    {
		base.Update();

    }

    public override bool IsTargetable(Unit unit)
    {
		if (null == unit)
			return false;

		if (null != unit.GetComponent<Unit>())
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}