using UnityEngine;



[System.Serializable]
[CreateAssetMenu(fileName = "New Potion Item", menuName = "Inventory System/Item/Potion Item")]
public class PotionItem : Item, IStackableItem
{
	[SerializeField]
	private int amount;
	IStackableItem prevStackableItem;
	IStackableItem nextStackableItem;


	public PotionItem(ItemDatum itemDatum, int amount) : base(itemDatum)
	{
		this.amount = amount;
	}

	public int GetAmount()
	{
		return amount;
	}

	public int GetMaxAmount()
	{
		var potionItemDatum = itemDatum as PotionItemDatum;
		if (null != potionItemDatum)
		{
			return potionItemDatum.GetMaxAmount();
		}
		
		Debug.Assert(false);
		return 0;
	}
	public int GetTotalPrice()
	{
		return GetPrice() * amount;
	}

	public IStackableItem GetNextSameItem()
	{
		return nextStackableItem;
	}

	public IStackableItem GetPrevSameItem()
	{
		return prevStackableItem;
	}
	public IStackableItem GetLastSameItem()
	{
		IStackableItem currentStackableItem = this;
		if (null != currentStackableItem.GetNextSameItem())
		{
			currentStackableItem = currentStackableItem.GetNextSameItem();
		}
		return currentStackableItem;
	}

	public void SetNextSameItem(IStackableItem nextStackableItem)
	{
		this.nextStackableItem = nextStackableItem;
	}

	public void SetPrevSameItem(IStackableItem prevStackableItem)
	{
		this.prevStackableItem = prevStackableItem;
	}
	public void DoublyLinkNextSameItem(IStackableItem nextStackableItem)
	{
		this.nextStackableItem = nextStackableItem;
		nextStackableItem.SetPrevSameItem(this);
	}

	public void AddAmount(int addedAmount, out int outAmount)
	{
		int remainingAmountToMaxAmount = GetMaxAmount() - amount;
		if (remainingAmountToMaxAmount < addedAmount)
			outAmount = addedAmount - remainingAmountToMaxAmount;
		else
		{
			outAmount = 0;
		}
	}

	public void SetAmount(int amount)
	{
		this.amount = amount;
	}
}
