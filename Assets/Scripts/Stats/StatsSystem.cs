using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsSystem 
{

	private int baseAttackPower;
	private int baseArmor;
	private int baseMaxHealthPoints;
	private float baseMoveSpeed;
	private float baseRange;

	private int addedAttackPower;
	private int addedArmor;
	private int addedMaxHeatlhPoints;
	private float addedMoveSpeed;
	private float addedRange;


	private int totalAttackPower;
	private int totalMaxHealthPoints;
	private int totalArmor;
	private float totalMoveSpeed;
	private float totalRange;

	private int currentHealthPoints;

	public StatsSystem(StatsDatum statsDatum)
	{
		baseAttackPower = statsDatum.GetBaseAttackPower();
		baseArmor = statsDatum.GetBaseArmor();
		baseMaxHealthPoints = statsDatum.GetBaseMaxHealthPoints();
		baseMoveSpeed = statsDatum.GetBaseMoveSpeed();
		baseRange = statsDatum.GetBaseRange();
		Calculate();

	}

	public StatsSystem(PlayerStatsDatum playerStatsDatum, int level)
    {
		int levelMinusOne = level - 1;
		baseAttackPower = playerStatsDatum.GetBaseAttackPower() + levelMinusOne * playerStatsDatum.GetAddedAttackPowerPerLevelUp();
		baseArmor = playerStatsDatum.GetBaseArmor();
		baseMaxHealthPoints = playerStatsDatum.GetBaseMaxHealthPoints() + levelMinusOne * playerStatsDatum.GetAddedMaxHealthPointsPerLevelUp();
		baseMoveSpeed = playerStatsDatum.GetBaseMoveSpeed();
		baseRange = playerStatsDatum.GetBaseRange();
		Calculate();
    }

	private void Calculate()
	{
		CalculateAttackPower();
		CalculateArmor();
		CalculateMaxHealthPoints();
		CalculateMoveSpeed();
	}

	public void AddAttackPower(int attackPower)
    {
		addedAttackPower += attackPower;
		CalculateAttackPower();
    }

	public void AddArmor(int armor)
    {
		addedArmor += armor;
		CalculateArmor();
    }

	public void AddMaxHealthPoints(int maxHealthPoints)
    {
		addedMaxHeatlhPoints += maxHealthPoints;
		CalculateMaxHealthPoints();
    }

	public void AddMoveSpeed(float moveSpeed)
    {
		addedMoveSpeed += moveSpeed;
		CalculateMoveSpeed();
    }

	public void AddRange(float range)
    {
		addedRange += range;
		CalculateRange();
    }

	private void CalculateAttackPower()
    {
		totalAttackPower = baseAttackPower + addedAttackPower;
		
		if (totalAttackPower < 1)
			totalAttackPower = 1;
	}

	private void CalculateArmor()
	{
		totalArmor = baseArmor + addedArmor;
		if (totalArmor < 0)
			totalArmor = 0;
	}

	private void CalculateMaxHealthPoints()
    {
		totalMaxHealthPoints = baseMaxHealthPoints + addedMaxHeatlhPoints;
		if (totalMaxHealthPoints < 1)
			totalMaxHealthPoints = 1;
	}
	private void CalculateMoveSpeed()
	{
		totalMoveSpeed = baseMoveSpeed + addedMoveSpeed;
		if (totalMoveSpeed < 1f)
			totalMoveSpeed = 1f;
	}

	private void CalculateRange()
    {
		totalRange = baseRange + addedRange;
    }

	public int GetBaseAttackPower()
    {
		return baseAttackPower;
    }

	public int GetBaseArmor()
    {
		return baseArmor;
    }
	
	public int GetBaseMaxHealthPoints()
    {
		return baseMaxHealthPoints;
    }

	public float GetBaseMoveSpeed()
    {
		return baseMoveSpeed;
    }

	public int GetTotalAttackPower()
	{
		return totalAttackPower;
	}
	public int GetTotalMaxHealthPoints()
    {
		return totalMaxHealthPoints;
    }
	public int GetTotalArmor()
	{
		return totalArmor;
	}	

	public float GetTotalMoveSpeed()
    {
		return totalMoveSpeed;
    }



	public void CalculateDamage(int damage, out int calculatedDamage)
	{
		calculatedDamage = damage - totalArmor;
		if (calculatedDamage < 1)
			calculatedDamage = 1;
	}

}
