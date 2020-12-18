
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : Unit
{
	[SerializeField]
	StatsDatum statsDatum;

	[SerializeField]
	protected string characterName;

	private Dictionary<string, float> clipLengths = new Dictionary<string, float>();

	//public Action[] SkillAction = new Action[SkillActionCount];


	private Transform selectCircle;
	protected override void Awake()
	{
		base.Awake();
		statsSystem.Init(5, 1);
		moveSystem.Init(new StraightMoveStrategy(this), 4f);
		healthPointsSystem.Init(300);
		skillSystem = new SkillSystem(this);
		
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

		base.Update();
	}

	public string GetCharacterName()
	{
		return characterName;
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