using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Enemy : Unit
{
    protected override void Awake()
	{
		base.Awake();
		targetingStrategy = new TargetingStrategy<Player>();
	}
}