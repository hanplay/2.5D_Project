using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
	Sword,
	Staff,
	Overall,
	Ring,
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Equipment Item Datum", menuName = "Inventory System/ItemDatum/Equipment Item Datum")]
public class EquipmentItemDatum : ItemDatum, ISerializationCallbackReceiver
{
	[SerializeField]
	private EquipmentType equipmentType;
	[SerializeField]
	private int levelLimit;

	public EquipmentType GetEquipmentType()
	{
		return equipmentType;
	}
	public int GetLevelLimit()
	{
		return levelLimit;
	}

	public void OnAfterDeserialize()
	{
		itemType = ItemType.Equipment;
	}

	public void OnBeforeSerialize()
	{
		
	}
}
