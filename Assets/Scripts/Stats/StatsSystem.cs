using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsSystem 
{
	private EquipmentSystem equipmentSystem;
	private LevelSystem levelSystem;
	
	private int baseAttackPower;
	private int baseArmor;

	private int addedAttackPower;
	private int addedArmor;

	private int percentAttackPower;

	private int totalAttackPower;
	private int totalArmor;	
	

	public StatsSystem(StatsDatum statsDatum, EquipmentSystem equipmentSystem, LevelSystem levelSystem)
	{
		baseAttackPower = statsDatum.GetBaseAttackPower();
		baseArmor = statsDatum.GetArmor();
		percentAttackPower = statsDatum.GetPercentAttackPower();
		this.equipmentSystem = equipmentSystem;
		this.levelSystem = levelSystem;
	}

	private void Calculate()
	{
		totalAttackPower = baseAttackPower + addedAttackPower;
		totalAttackPower += totalAttackPower * percentAttackPower / 100;
		totalArmor = baseArmor + addedArmor;
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
		calculatedDamage = damage;
	}

}
