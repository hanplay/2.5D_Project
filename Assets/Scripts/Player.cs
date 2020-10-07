using System;
using UnityEngine;

public abstract class Player : Unit
{
	public const int SkillCount = 4;
	public const int SkillActionCount = 4;

	[SerializeField]
	protected string characterName;



	[SerializeField]
	protected EquipmentSystem equipmentSystem;
	
	[SerializeField]
	protected LevelSystem levelSystem;
	[SerializeField]
	protected StatsDatum statsDatum;
	
	protected HealthPointsSystem healthPointsSystem;
	protected StatsSystem statsSystem;

	private TravelRouteWriter travelRouteWriter;

	[SerializeField]
    private SkillState[] skillList = new SkillState[SkillCount];
	public Action[] SkillAction = new Action[SkillActionCount];


	private State state;
	#region State 
	private IdleState idleState;
	private MoveToGroundState moveToGroundState;
	private BaseAttackState baseAttackState;
	private ChaseTargetState chaseTargetState;

	public IdleState GetIdleState()
	{
		return idleState;
	}
	public MoveToGroundState GetMoveToGroundState(Vector3 destination)
	{
		moveToGroundState.SetDestination(destination);
		return moveToGroundState;
	}
	public BaseAttackState GetBaseAttackState()
	{
		return baseAttackState;
	}
	public ChaseTargetState GetChaseTargetState()
	{
		return chaseTargetState;
	}

	public BasicState ProperBasicState()
	{
		if (TargetUnitExist())
		{
			if (baseAttackState.IsTargetUnitInRange())
			{
				return baseAttackState;
			}
			else
			{
				return chaseTargetState;
			}
		}
		else
		{
			return idleState;
		}
	}

	public void SetCurrentState(State state)
	{
		this.state = state;
	}
	public State GetCurrentState()
	{
		return state;
	}

	public void SetNextState(State nextState)
	{
		state.SetNextState(nextState);
	}

	#endregion
	protected virtual void Awake()
    {	
        base.Awake();
		levelSystem = new LevelSystem();
		healthPointsSystem = new HealthPointsSystem(statsDatum, equipmentSystem, levelSystem);
		statsSystem = new StatsSystem(statsDatum, equipmentSystem, levelSystem);
		travelRouteWriter = GetComponent<TravelRouteWriter>();
		#region State 
		state = idleState = new IdleState(this);
		moveToGroundState = new MoveToGroundState(this);
		baseAttackState = new BaseAttackState(this);
		chaseTargetState = new ChaseTargetState(this);
		#endregion
	}

	
	protected void Update()
    {
		base.Update();
    }

	private void FixedUpdate()
    {
		state.Tick(Time.fixedDeltaTime);
	}

	public override void BeDamaged(int damage)
	{
		int calculatedDamage;
		statsSystem.CalculateDamage(damage, out calculatedDamage);
		healthPointsSystem.SubtractHealthPoints(calculatedDamage);
	}
	public override HealthPointsSystem GetHealthPointsSystem()
	{
		return healthPointsSystem;
	}

	public string GetCharacterName()
	{
		return characterName;
	}

	public abstract bool IsTargetable(Unit unit);

	public LevelSystem GetLevelSystem()
	{
		return levelSystem;
	}

	public TravelRouteWriter GetTravelRouteWriter()
    {
		return travelRouteWriter;
    }

	public SkillState GetSkill(int i)
    {
		return skillList[i];
    }

	public int GetSkillCount()
    {
		return skillList.Length;
    }
}