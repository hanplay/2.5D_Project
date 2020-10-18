using System;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.VisualBasic;

public abstract class Player : Unit
{
	public const int SkillCount = 4;
	public const int SkillActionCount = 4;

	[SerializeField]
	protected SkillData skillData;

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

	[SerializeField]
    protected Skill[] skillList = new Skill[SkillCount];
	public Action[] SkillAction = new Action[SkillActionCount];

	private Command command;
    #region Command
    public void SetCommand(Command command)
    {
		this.command = command;
    }
    #endregion
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

		command = new NullCommand(this);

		#region State 
		state = idleState = new IdleState(this);
		attackState = new AttackState(this);
		moveState = new MoveState(this);
        #endregion

        RuntimeAnimatorController runtimeAnimatorController = transform.Find("model").GetComponent<Animator>().runtimeAnimatorController;
        AnimationClip[] animationClips = runtimeAnimatorController.animationClips;
		
		
        for (int i = 0; i < animationClips.Length; i++)
        {
            clipLengths.Add(animationClips[i].name, animationClips[i].length);
        }
    }

	private Command commandBuffer;
	protected void Update()
    {
		if(command != commandBuffer)
        {
			commandBuffer = command;
			Debug.Log(command.ToString());
        }
		base.Update();
    }
	protected void FixedUpdate()
    {
		state.TickAccept(Time.deltaTime, command);
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

	public Skill GetSkill(int i)
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