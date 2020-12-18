using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	protected BuffSystem buffSystem;
	protected StateSystem stateSystem;

	//아래는 하위 클래스에서 Init으로 초기화가 필요함
	protected StatsSystem statsSystem;
	protected MoveSystem moveSystem;
	protected AttackSystem attackSystem;
	protected HealthPointsSystem healthPointsSystem;	
	protected SkillSystem skillSystem;


	protected virtual void Awake()
    {
		healthPointsSystem = new HealthPointsSystem(this);
		moveSystem = new MoveSystem();
		buffSystem = new BuffSystem(this);
		attackSystem = new AttackSystem(this);
		statsSystem = new StatsSystem();
		stateSystem = new StateSystem(this);	

		skillSystem = null;
    }

    protected virtual void Update()
	{
		buffSystem.Tick(Time.deltaTime);
		stateSystem.Tick(Time.deltaTime);
		skillSystem?.Tick(Time.deltaTime);
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

	public SkillSystem GetSkillSystem()
    {
		return skillSystem;
    }

	public HealthPointsSystem GetHealthPointsSystem()
    {
		return healthPointsSystem;
    }

	public abstract bool IsTargetable(Unit unit);

}