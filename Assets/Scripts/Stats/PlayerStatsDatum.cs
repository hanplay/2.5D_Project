using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats Datum", menuName = "Stats System/New Player Stats Datum")]
public class PlayerStatsDatum : StatsDatum
{
	[SerializeField]
	private int addedAttackPowerPerLevelUp;

	[SerializeField]
	private int addedMaxHealthPointsPerLevelUp;

	public int GetAddedAttackPowerPerLevelUp()
	{
		return addedAttackPowerPerLevelUp;
	}

	public int GetAddedMaxHealthPointsPerLevelUp()
	{
		return addedMaxHealthPointsPerLevelUp;
	}
}
