using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{

	public event EventHandler OnDieEvent;


	protected StatsSystem statsSystem;
	protected BuffSystem buffSystem;
	protected HealthPointsSystem healthPointsSystem;

	protected IAttackStrategy attackStrategy;

	protected void Awake()
    {
		buffSystem = new BuffSystem(this);
    }

    protected virtual void Update()
	{
		buffSystem.Tick(Time.deltaTime);
	}
	
	public Vector3 GetPosition()
    {
		return transform.position;
    }


	public void SetPosition(Vector3 position)
	{
		transform.position = position;
	}

	public void FlipLeft()
	{
		transform.localScale = Vector3.one;
	}

	public void FlipRight()
	{
		transform.localScale = new Vector3(-1f, 1f, 1f);
	}

    public float DistanceToUnit(Unit unit)
    {
		return Vector3.Distance(transform.position, unit.GetPosition());
    }

	public Vector3 DirectionToUnit(Unit unit)
	{
		Vector3 direction = unit.GetPosition() - transform.position;
		direction.Normalize();
		return direction;
	}



	public StatsSystem GetStatsSystem()
    {
		return statsSystem;
    }

	public BuffSystem GetBuffSystem()
    {
		return buffSystem;
    }

	public HealthPointsSystem GetHealthPointsSystem()
    {
		return healthPointsSystem;
    }

	public abstract bool IsTargetable(Unit unit);

	public void OnDie()
    {
		OnDieEvent.Invoke(this, EventArgs.Empty);
    }

	public IAttackStrategy GetAttackStrategy()
	{
		return attackStrategy;
	}

}