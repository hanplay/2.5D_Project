using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	protected BuffSystem buffSystem;
	protected StateSystem stateSystem;
	protected ChaserContainer chaserContainer;
	protected TargetedUnitHandler targetedUnitHandler;

	//아래는 하위 클래스에서 Init으로 초기화가 필요함
	protected StatsSystem statsSystem;
	protected MoveSystem moveSystem;
	
	protected HealthPointsSystem healthPointsSystem;
	//하위클래스에서 생성함
	protected AttackStrategy attackStrategy;
	protected ITargetingStrategy targetingStrategy;
	//하위 클래스가 쓸지 안쓸지 생성자 생성으로 결정
	protected SkillSystem skillSystem;

	private Transform modelTranform;


	protected virtual void Awake()
    {
		
		moveSystem = new MoveSystem();
		targetedUnitHandler = new TargetedUnitHandler(this);
		healthPointsSystem = new HealthPointsSystem(this);
		chaserContainer = new ChaserContainer(this);
		stateSystem = new StateSystem(this);	
		statsSystem = new StatsSystem();
		buffSystem = new BuffSystem(this);

		skillSystem = null;

		modelTranform = transform.Find("model");
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

	public SkillSystem GetSkillSystem()
    {
		return skillSystem;
    }

	public HealthPointsSystem GetHealthPointsSystem()
    {
		return healthPointsSystem;
    }

	public bool IsTargetable(Unit unit)
    {
		return targetingStrategy.IsTargetable(unit);
    }

	public TargetedUnitHandler GetTargetedUnitHandler()
    {
		return targetedUnitHandler;
    }

	public ChaserContainer GetChaserContainer()
    {
		return chaserContainer;
    }

	public AttackStrategy GetAttackStrategy()
    {
		return attackStrategy;
    }

	public ITargetingStrategy GetTargetingStrategy()
    {
		return targetingStrategy;
    }

	public void FlipLeft()
    {
		modelTranform.localScale = Vector3.one;
    }

	public void FlipRight()
    {
		Vector3 rightScale = new Vector3(-1, 1, 1);
		modelTranform.localScale = rightScale;
    }

	public void FlipToTarget(Unit targetedUnit)
	{
		if (GetPosition().x < targetedUnit.GetPosition().x)		
			FlipRight();		
		else	
			FlipLeft();		
	}

	public void FlipToTarget(Vector3 destination)
	{
		if (GetPosition().x < destination.x)		
			FlipRight();		
		else		
			FlipLeft();		
	}

}