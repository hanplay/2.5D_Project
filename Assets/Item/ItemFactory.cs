using System.Collections;
using UnityEngine;

public class ItemFactory 
{
	public Item MakeItem(ItemDatum itemDatum)
	{
		switch(itemDatum.GetItemType())
		{
		case ItemType.Equipment:
			return new EquipmentItem(itemDatum);
		case ItemType.Etc:
			return new EtcItem(itemDatum, 1);
		case ItemType.Potion:
			return new PotionItem(itemDatum, 1);
		default:
			Debug.Log("Item Factory Default Item Type Error");
			Debug.Assert(false);
			return new Item(itemDatum);
		}
	}
	public Item MakeItem(ItemDatum itemDatum, int amount)
	{
		switch (itemDatum.GetItemType())
		{
		case ItemType.Etc:
			return new EtcItem(itemDatum, amount);
		case ItemType.Potion:
			return new PotionItem(itemDatum, amount);
		default:
			Debug.Log("Item Factory Default Item Type Error : Stackable");
			Debug.Assert(false);
			return new Item(itemDatum);
		}
	}

	public Item MakeItem(IStackableItemDatum stackableItemDatum, int amount)
	{
		ItemDatum itemDatum = stackableItemDatum as ItemDatum;
		switch (itemDatum.GetItemType())
		{
		case ItemType.Etc:
			return new EtcItem(itemDatum, amount);
		case ItemType.Potion:
			return new PotionItem(itemDatum, amount);
		default:
			Debug.Log("Item Factory Default Item Type Error : Stackable");
			Debug.Assert(false);
			return new Item(itemDatum);
		}
	}

}
