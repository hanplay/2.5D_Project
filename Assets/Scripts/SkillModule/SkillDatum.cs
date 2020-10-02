using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
[CreateAssetMenu(fileName = "New Skill Datum", menuName = "Skill System/Skill Datum")]
public class SkillDatum : ScriptableObject
{
	
	protected SkillType skillType;
	[SerializeField]
	private Sprite sprite;
	[SerializeField]
	private float cooldown;
	[SerializeField]
	private string stateName;

	public Sprite GetSprite()
	{
		return sprite;
	}
	public float GetCooldown()
	{
		return cooldown;
	}
	
	public string GetStateName()
	{
		return stateName;
	}

	public SkillType GetSkillType()
	{
		return skillType;
	}
}
