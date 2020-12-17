
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
	protected SkillSystem skillSystem;
	[SerializeField]
	protected EquipmentSystem equipmentSystem;
	[SerializeField]
	protected LevelSystem levelSystem;
	[SerializeField]
	protected PlayerStatsDatum playerStatsDatum;

	[SerializeField]
	protected Skill[] skillList = new Skill[SkillCount];
	public Action[] SkillAction = new Action[SkillActionCount];


	private Transform selectCircle;
	protected override void Awake()
	{
		skillSystem = new SkillSystem(this);
		levelSystem = new LevelSystem();
		statsSystem = new StatsSystem(playerStatsDatum, levelSystem.GetLevel());
		moveSystem = new MoveSystem(this, playerStatsDatum.GetBaseMoveSpeed());
		healthPointsSystem = new PlayerHealthPointsSystem(playerStatsDatum.GetBaseMaxHealthPoints(),
		playerStatsDatum.GetAddedMaxHealthPointsPerLevelUp(), levelSystem);
		base.Awake();

		selectCircle = transform.Find("SelectCircle");
		HideSelectCircle();


		RuntimeAnimatorController runtimeAnimatorController = transform.Find("model").GetComponent<Animator>().runtimeAnimatorController;
		AnimationClip[] animationClips = runtimeAnimatorController.animationClips;

		for (int i = 0; i < animationClips.Length; i++)
		{
			clipLengths.Add(animationClips[i].name, animationClips[i].length);
			print(animationClips[i].name + ": " + animationClips[i].length);
		}
	}

	protected override void Update()
	{
		for (int i = 0; i < skillList.Length; i++)
		{
			skillList[i]?.Tick(Time.deltaTime);
		}
		base.Update();
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

	public void ShowSelectCircle()
	{
		selectCircle.gameObject.SetActive(true);
	}

	public void HideSelectCircle()
	{
		selectCircle.gameObject.SetActive(false);
	}
}