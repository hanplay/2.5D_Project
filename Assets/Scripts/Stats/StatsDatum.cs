using UnityEngine;

[CreateAssetMenu(fileName = "New Stats Datum", menuName = "Stats System/New Stats Datum")]
public class StatsDatum : ScriptableObject
{
	[SerializeField]
	private int baseAttackPower;
	[SerializeField]
	private int addedAttackPowerPerLevelUp;

	[SerializeField]
	private int baseMaxHealthPoints;
	[SerializeField]
	private int addedMaxHealthPointsPerLevelUp;


	[SerializeField]
	private int percentAttackPower;

	[SerializeField]
	private int armor;



	public int GetBaseAttackPower()
	{
		return baseAttackPower;
	}
	public int GetAddedAttackPowerPerLevelUp()
	{
		return addedAttackPowerPerLevelUp;
	}

	public int GetBaseMaxHealthPoints()
	{
		return baseMaxHealthPoints;
	}
	public int GetAddedMaxHealthPointsPerLevelUp()
	{
		return addedMaxHealthPointsPerLevelUp;
	}

	public int GetPercentAttackPower()
	{
		return percentAttackPower;
	}
	public int GetArmor()
	{
		return armor;
	}
}
