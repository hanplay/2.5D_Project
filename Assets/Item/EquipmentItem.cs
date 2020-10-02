using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory System/Item/Equipment Item")]
public class EquipmentItem : Item
{
	public EquipmentItem(ItemDatum itemDatum) : base(itemDatum) { }

	public EquipmentType GetEquipmentType()
	{
		EquipmentItemDatum equipmentItemDatum = itemDatum as EquipmentItemDatum;
		if(null != equipmentItemDatum)
		{
			return equipmentItemDatum.GetEquipmentType();
		}

		Debug.LogError("Equipment Item Constructor Error");
		Debug.Assert(false);
		return 0;
	}
}
