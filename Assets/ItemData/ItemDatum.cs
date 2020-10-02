using UnityEngine;

public enum ItemType
{
	Potion,
	Equipment,
	Etc
}
[System.Serializable]
public class ItemDatum : ScriptableObject
{
	
	[SerializeField]
	protected int id;
	[SerializeField]
	protected ItemType itemType;
	[SerializeField]
	protected Sprite sprite;
	[SerializeField]
	protected int price;

	public void SetId(int id)
	{
		this.id = id;
	}

	public int GetId()
	{
		return id;
	}
	public ItemType GetItemType()
	{
		return itemType;
	}

	public Sprite GetSprite()
	{
		return sprite;
	}

	public int GetPrice()
	{
		return price;
	}

	public bool IsStackable()
	{
		switch (itemType)
		{
		case ItemType.Equipment:
			return false;
		case ItemType.Etc:
		case ItemType.Potion:
			return true;
		default:
			Debug.Log("Get Item Type Error");
			Debug.Assert(false);
			return false;
		}

	}
}
