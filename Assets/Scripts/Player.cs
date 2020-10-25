﻿using System;
using System.Collections.Generic;
using UnityEngine;	

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
	protected PlayerStatsDatum playerStatsDatum;

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
	private MoveState moveState;
	private ChaseState chaseState;
	private AttackState attackState;
	private DieState dieState;


	public IdleState GetIdleState()
	{
		return idleState;
		
	}
	public MoveState GetMoveState()
    {
		return moveState;
    }
	public ChaseState GetChaseState(Unit targetUnit)
    {
		chaseState.SetTargetUnit(targetUnit);
		return chaseState;
    }
	public AttackState GetAttackState(Unit targetUnit)
    {
		attackState.SetTargetUnit(targetUnit);
		return attackState;
    }

	public DieState GetDieState()
    {
		return dieState;
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
		levelSystem = new LevelSystem();
		statsSystem = new StatsSystem(playerStatsDatum, levelSystem.GetLevel());
		healthPointsSystem = new HealthPointsSystem(statsSystem.GetTotalMaxHealthPoints());

		command = new NullCommand(this);

		#region State 
		state = idleState = new IdleState(this);
		moveState = new MoveState(this);
		chaseState = new ChaseState(this);
		attackState = new AttackState(this);
		dieState = new DieState(this, 5f);
		state.Begin();
        #endregion

        RuntimeAnimatorController runtimeAnimatorController = transform.Find("model").GetComponent<Animator>().runtimeAnimatorController;
        AnimationClip[] animationClips = runtimeAnimatorController.animationClips;
		
        for (int i = 0; i < animationClips.Length; i++)
        {
            clipLengths.Add(animationClips[i].name, animationClips[i].length);
			print(animationClips[i].name + ": " + animationClips[i].length);
        }
    }

	private Command commandBuffer;
	private State stateBuffer;
	protected void Update()
    {
		//if(command != commandBuffer)
  //      {
		//	commandBuffer = command;
		//	Debug.Log(command.ToString());
  //      }
		if(state != stateBuffer)
        {
			stateBuffer = state;
			Debug.Log(state.ToString());
        }

		state.TickAccept(Time.deltaTime, command);
		for(int i = 0; i < skillList.Length; i++)
        {
			skillList[i]?.Tick(Time.deltaTime);
        }
		base.Update();
    }
	protected void FixedUpdate()
    {
	}

	public string GetCharacterName()
	{
		return characterName;
	}



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