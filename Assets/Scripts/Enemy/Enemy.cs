using UnityEngine;
using System;
using System.Collections.Generic;

public class Enemy : Unit
{
	public event EventHandler OnRest;
	private State prevState;
	private float lagTime = 0;
	private ChaseCommand chaseCommand = new ChaseCommand();
	private Player prevPlayer;

    protected override void Awake()
	{
		base.Awake();
		statsSystem.Init(4, 0);
		healthPointsSystem.Init(200);
		moveSystem.Init(new StraightMoveStrategy(this), 3f);
		attackSystem.Init(new InstantAttackStrategy(this, new CommonDamageStrategy()), 1.5f);
		targetingStrategy = new TargetingStrategy<Player>();
		prevState = stateSystem.GetCurrentState();
	}
	
	protected override void Update()
    {
		lagTime += Time.deltaTime;
		base.Update();
		if (stateSystem.GetCurrentState() == stateSystem.GetAttackState())
			return;
		if (lagTime < 3.5f)
			return;

		Collider[] colliders = Physics.OverlapSphere(GetPosition(), 10f);
		for(int i = 0; i < colliders.Length; i++)
		{ 			
			if (colliders[i].TryGetComponent(out Player player))
            {				
				chaseCommand.Execute(this, player);
				lagTime = 0f;
				prevState = stateSystem.GetCurrentState();
            }
        }
    }

}