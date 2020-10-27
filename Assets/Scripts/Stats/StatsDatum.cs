using UnityEngine;

[CreateAssetMenu(fileName = "New Stats Datum", menuName = "Stats System/New Stats Datum")]
public class StatsDatum : ScriptableObject
{
	[SerializeField]
	private int baseAttackPower;
	[SerializeField]
	private int baseMaxHealthPoints;

	[SerializeField]
	private int baseArmor;

	[SerializeField]
	private float baseMoveSpeed;

	[SerializeField]
	private float baseRange;

	public int GetBaseAttackPower()
	{
		return baseAttackPower;
	}

	public int GetBaseMaxHealthPoints()
	{
		return baseMaxHealthPoints;
	}

	public int GetBaseArmor()
	{
		return baseArmor;
	}

	public float GetBaseMoveSpeed()
    {
		return baseMoveSpeed;
    }

	public float GetBaseRange()
    {
		return baseRange;
    }
}
