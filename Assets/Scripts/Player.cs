using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Unit
{
	public const int SkillCount = 4;
	public const int SkillActionCount = 4;

	[SerializeField]
	protected string characterName;

	private Dictionary<string, float> clipLengths = new Dictionary<string, float>();



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

	private InputBuffer inputHandler;
	public InputBuffer GetInputHandler()
    {
		//if(null == inputHandler)
  //      {
		//	print("input Handler assign!");
		//	inputHandler = new InputHandler(this);
  //      }
		return inputHandler;
    }



	private State state;
	#region State 
	private IdleState idleState;
	private AttackState attackState;
	private MoveState moveState;


	public IdleState GetIdleState()
	{
		return idleState;
	}

	public AttackState GetAttackState()
    {
		return attackState;
    }

	public MoveState GetMoveState()
    {
		return moveState;
    }

	public State GetState()
	{
		return state;
	}

	public void SetState(State state)
    {
		this.state = state;
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
		inputHandler = new InputBuffer(this);
		state = idleState = new IdleState(this);
		attackState = new AttackState(this);
		moveState = new MoveState(this);
        #endregion

        RuntimeAnimatorController runtimeAnimatorController = transform.Find("model").GetComponent<Animator>().runtimeAnimatorController;
        AnimationClip[] animationClips = runtimeAnimatorController.animationClips;
		
		
        for (int i = 0; i < animationClips.Length; i++)
        {
            clipLengths.Add(animationClips[i].name, animationClips[i].length);
            Debug.Log(animationClips[i].name + ": " + animationClips[i].length);

        }
    }

	
	protected void Update()
    {
		base.Update();
    }
	protected void FixedUpdate()
    {
		state.Tick(Time.deltaTime);
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

	public float GetClipLength(string name)
    {
		return clipLengths[name];
    }
}