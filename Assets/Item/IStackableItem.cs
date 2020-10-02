
public interface IStackableItem 
{

	ItemDatum GetItemDatum();
	int GetTotalPrice();
	int GetAmount();
	int GetMaxAmount();
	void SetAmount(int amount);
	void AddAmount(int addedAmount, out int outAmount);
	void SetNextSameItem(IStackableItem nextStackableItem);
	void SetPrevSameItem(IStackableItem prevStackableItem);
	IStackableItem GetLastSameItem();
	IStackableItem GetNextSameItem();
	IStackableItem GetPrevSameItem();
	void DoublyLinkNextSameItem(IStackableItem nextStackableItem);
}