using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	public event EventHandler OnDead;

	private List<Buff> buffs = new List<Buff>();

	public Action BaseAttackAction;

	private BaseAttackStrategy baseAttackStrategy = new BaseAttackStrategy();

	protected StatsSystem statsSystem;
	protected HealthPointsSystem healthPointsSystem;

	public BaseAttackStrategy GetBaseAttackStrategy() 
	{
		return baseAttackStrategy;
	}


    protected void Update()
	{
		for(int i = 0; i < buffs.Count; i++)
        {
			buffs[i].Tick(Time.deltaTime);
			if(buffs[i].IsEnded())
            {
				buffs.RemoveAt(i);
            }
        }
	}
	
	public Vector3 GetPosition()
    {
		return transform.position;
    }


	public void SetPosition(Vector3 position)
	{
		transform.position = position;
	}

	public void Die()
	{				
		OnDead?.Invoke(this, EventArgs.Empty);
		Destroy(gameObject);
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

	public void AddBuff(Buff buff)
    {
		buffs.Add(buff);
    }

	public StatsSystem GetStatsSystem()
    {
		return statsSystem;
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

	public void Heal(int healingHealthPoints)
    {
		healthPointsSystem.AddHealthPoints(healingHealthPoints);
    }

}