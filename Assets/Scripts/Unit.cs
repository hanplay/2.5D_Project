using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	protected BuffSystem buffSystem;
	protected StateSystem stateSystem;

	//하위 클래스에서 초기화
	protected StatsSystem statsSystem;
	protected MoveSystem moveSystem;
	protected AttackSystem attackSystem;
	protected HealthPointsSystem healthPointsSystem;
	

    protected virtual void Awake()
    {
		stateSystem = new StateSystem(this);	
		buffSystem = new BuffSystem(this);	
    }

    protected virtual void Update()
	{
		buffSystem.Tick(Time.deltaTime);
		stateSystem.Tick(Time.deltaTime);
	}
	
	public Vector3 GetPosition()
    {
		return transform.position;
    }

	public void SetPosition(Vector3 position)
	{
		transform.position = position;
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
	public BuffSystem GetBuffSystem()
    {
		return buffSystem;
    }

	public StateSystem GetStateSystem()
    {
		return stateSystem;
    }

	public StatsSystem GetStatsSystem()
    {
		return statsSystem;
    }
	public MoveSystem GetMoveSystem()
    {
		return moveSystem;
    }

	public AttackSystem GetAttackSystem()
    {
		return attackSystem;
    }

	public HealthPointsSystem GetHealthPointsSystem()
    {
		return healthPointsSystem;
    }

	public abstract bool IsTargetable(Unit unit);

}