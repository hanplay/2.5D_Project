using System.Collections;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item/Item")]
public class Item : ScriptableObject
{
	[SerializeField]
	protected ItemDatum itemDatum;

	public Item(ItemDatum itemDatum)
	{
		this.itemDatum = itemDatum;
	}
	public ItemType GetItemType()
	{
		return itemDatum.GetItemType();
	}
	public ItemDatum GetItemDatum()
	{
		return itemDatum;
	}
	public  Sprite GetSprite()
	{
		return itemDatum.GetSprite();
	}
	public bool IsStackable()
	{
		return itemDatum.IsStackable();
	}

	public int GetPrice()
	{
		return itemDatum.GetPrice();
	}

}
