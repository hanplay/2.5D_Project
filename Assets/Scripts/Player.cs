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


	
	protected virtual void Awake()
    {	
        base.Awake();
		levelSystem = new LevelSystem();
		healthPointsSystem = new HealthPointsSystem(statsDatum, equipmentSystem, levelSystem);
		statsSystem = new StatsSystem(statsDatum, equipmentSystem, levelSystem);
		travelRouteWriter = GetComponent<TravelRouteWriter>();
	}

	
	protected void Update()
    {
		base.Update();
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