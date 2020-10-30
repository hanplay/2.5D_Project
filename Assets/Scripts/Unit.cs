using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	public event EventHandler OnAttackBegin;
	public event EventHandler OnAttackEnd;
	public event EventHandler OnSkillBegin;
	public event EventHandler OnSkillEnd;

	public event EventHandler OnDieEvent;

	

	public Action BaseAttackAction;

	protected IAttackStrategy attackStrategy;

	protected StatsSystem statsSystem;
	protected BuffSystem buffSystem;
	protected HealthPointsSystem healthPointsSystem;

	public IAttackStrategy GetAttackStrategy() 
	{
		return attackStrategy;
	}

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
		Vector3 scale = Vector3.one;
		scale.x = -1f;
		transform.localScale = scale;
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

	public void BeDamaged(int damage)
	{
		int calculatedDamage;
		statsSystem.CalculateDamage(damage, out calculatedDamage);
		healthPointsSystem.SubtractHealthPoints(calculatedDamage);
	}

	public void BeTrueDamaged(int trueDamage)
    {
		healthPointsSystem.SubtractHealthPoints(trueDamage);
    }

	public void Heal(int healingHealthPoints)
    {
		healthPointsSystem.AddHealthPoints(healingHealthPoints);
    }

	public abstract bool IsTargetable(Unit unit);

	public void OnAttackBeginNotify()
    {
		OnAttackBegin?.Invoke(this, EventArgs.Empty);
    }

	public void OnAttackEndNotify()
    {
		OnAttackEnd?.Invoke(this, EventArgs.Empty);
    }

	public void OnSkillBeginNotify()
	{
		OnSkillBegin?.Invoke(this, EventArgs.Empty);
	}

	public void OnSkillEndNotify()
	{
		OnSkillEnd?.Invoke(this, EventArgs.Empty);
	}

	public void OnDie()
    {
		OnDieEvent.Invoke(this, EventArgs.Empty);
    }
}