using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsSystem 
{
	private int baseAttackPower;
	private int baseArmor;

	private int addedAttackPower;
	private int addedArmor;

	private int totalAttackPower;
	private int totalArmor;

	public StatsSystem() { }

	public void Init(StatsDatum statsDatum)
    {
		baseAttackPower = statsDatum.GetBaseAttackPower();
		baseArmor = statsDatum.GetBaseArmor();
		Calculate();
    }

	public void Init(int attackPower, int armor)
    {
		baseAttackPower = attackPower;
		baseArmor = armor;
    }


	private void Calculate()
	{
		CalculateAttackPower();
		CalculateArmor();
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

	public int GetBaseAttackPower()
    {
		return baseAttackPower;
    }

	public int GetBaseArmor()
    {
		return baseArmor;
    }
	
	public int GetTotalAttackPower()
	{
		return totalAttackPower;
	}

	public int GetTotalArmor()
	{
		return totalArmor;
	}	

	public void CalculateDamage(int damage, out int calculatedDamage)
	{
		calculatedDamage = damage - totalArmor;
		if (calculatedDamage < 1)
			calculatedDamage = 1;
	}
}
