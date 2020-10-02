using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemChanged;

    private string saveFolder;
    private string saveFile;
    private string savePath;

    public class OnItemChangedArgs : EventArgs
    {
        public int index;
    }


    [SerializeField]
    private List<Item> itemList;

    private Dictionary<IStackableItemDatum, IStackableItem> stackableItemDictionary = new Dictionary<IStackableItemDatum, IStackableItem>();
    
    [SerializeField]
    private int goldAmount;
    private int firstEmptyItemIndex;
    private int count; //현재 inventory(itemList)내의 item 개수
    public const int MAX_ITEM_COUNT = 25;

       
    private Item selectedItem;
    private int selectedItemIndex = -1; //selected item is none

    private ItemFactory itemFactory = new ItemFactory();
    public event EventHandler OnReload;

            

    private void Awake()
    {
        Trim();
    }

    private void OnEnable()
    {
        Selector.GetInstance().OnSelect += Selector_OnSelect;
        Selector.GetInstance().OnUnSelect += Selector_OnUnSelect;        
    }


    private void Selector_OnSelect(object sender, EventArgs e)
    {
        Selector.OnSelectEventArgs onSelectEventArgs = e as Selector.OnSelectEventArgs;
        UI_Item uI_Item = onSelectEventArgs.selectableObject as UI_Item;
        if(null != uI_Item)
        {
            selectedItemIndex = uI_Item.GetIndex();
        }

        Debug.Log("inventory selectedItemIndex: " + selectedItemIndex);
    }

    private void Selector_OnUnSelect(object sender, EventArgs e)
    {
        selectedItemIndex = -1;
        Debug.Log("inventory selectedItemIndex: " + selectedItemIndex);
    }

    private void Trim()
    {
        int i;
        for(i = 0; i < itemList.Count; i++)
        {
            var stackableItem = itemList[i] as IStackableItem;
            if(null != stackableItem)
            {
                IStackableItemDatum stackableItemDatum = stackableItem.GetItemDatum() as IStackableItemDatum;
                if(null == stackableItemDictionary[stackableItemDatum])
                {
                    stackableItemDictionary[stackableItemDatum] = stackableItem;
                }
                else
                {
                    stackableItemDictionary[stackableItemDatum].GetLastSameItem().DoublyLinkNextSameItem(stackableItem);
                }
            }
            //itemDictionary.Add(itemList[i].GetItemDatum(), itemList[i]);
        }
        firstEmptyItemIndex = i;
        while(MAX_ITEM_COUNT > itemList.Count)
        {
            itemList.Add(null);
        }
        Debug.Log("Trim");
    }

    public Item GetItem(int i)
    {
        return itemList[i];
    }

    public void SetItem(int i, Item item)
    {
        if(i >= itemList.Count)
        {
            Debug.LogError("Item List Outof Inext Error");
        }
        //
        itemList[i] = item;
    }

    private void AddItem(IStackableItemDatum stackableItemDatum, int amount)
    {
        IStackableItem stackableItem = stackableItemDictionary[stackableItemDatum];
        IStackableItem lastSameStackableItem = stackableItem.GetLastSameItem();

        lastSameStackableItem.AddAmount(amount, out amount);
        if(amount > 0)
        {
            AddItemReculsive(lastSameStackableItem, amount);
        }
    }

    private void AddItemReculsive(IStackableItem currentStackableItem, int amount)
    {
        int maxAmount = currentStackableItem.GetMaxAmount();
        if(maxAmount <= amount)
        {
            Item newItem = itemFactory.MakeItem(currentStackableItem.GetItemDatum(), amount);
            itemList[firstEmptyItemIndex] = newItem;

            IStackableItem newStackableItem = newItem as IStackableItem;

            currentStackableItem.DoublyLinkNextSameItem(newStackableItem);

            FindNextFirstEmptyItemIndex();

            return;
        }
        else
        {
            Item newItem = itemFactory.MakeItem(currentStackableItem.GetItemDatum(), amount);
            itemList[firstEmptyItemIndex] = newItem;

            IStackableItem newStackableItem = newItem as IStackableItem;

            currentStackableItem.DoublyLinkNextSameItem(newStackableItem);            

            FindNextFirstEmptyItemIndex();

            AddItemReculsive(newStackableItem, amount - maxAmount);

            return;
        }
    }

    private void AddItem(ItemDatum itemDatum)
    {
        itemList[firstEmptyItemIndex] = itemFactory.MakeItem(itemDatum);
        FindNextFirstEmptyItemIndex();
    }

    public bool IsAffordable(ItemDatum itemDatum)
    {
        if (goldAmount < itemDatum.GetPrice())
        {
            return false;
        }

        else 
        {
            return true;
        }
    }

    public bool IsAffordable(ItemDatum itemDatum, int amount)
    {
        if (goldAmount < itemDatum.GetPrice() * amount)
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    public bool TryToBuyStackableItem(ItemDatum itemDatum, int orderAmount )
    {
        IStackableItemDatum stackableItemDatum = itemDatum as IStackableItemDatum;
        if (null != stackableItemDatum)
        {
            
            int maxAmount = stackableItemDatum.GetMaxAmount();
            int remainingItemSlots = MAX_ITEM_COUNT - count;
            int remainingStakcsToMaxAmount = maxAmount - stackableItemDictionary[stackableItemDatum].GetLastSameItem().GetAmount();
            int limitAmount = remainingItemSlots * maxAmount + remainingStakcsToMaxAmount;
            
            if (orderAmount > limitAmount)
                return false;
            else
            {
                AddItem(stackableItemDatum, orderAmount);                   
            }
        }
        return false; 
    }

    public bool TryToBuyEquipmentItem(ItemDatum itemDatum)
    {
        if (IsInventoryFull())
            return false;
        else
        {
            goldAmount -= itemDatum.GetPrice();
            AddItem(itemDatum);
            return true;
        }
        
    }

    public int GetFirstEmptyItemIndex()
    {
        return firstEmptyItemIndex;
    }

    public void SetFirstEmptyItemIndex(int firstEmptyItemIndex)
    {
        this.firstEmptyItemIndex = firstEmptyItemIndex;
    }
    
    public bool IsInventoryFull()
    {
        if(MAX_ITEM_COUNT <= firstEmptyItemIndex)
        {
            return true;
        }
        else
        {
            return false;   
        }
    }

    private void FindNextFirstEmptyItemIndex()
    {
        for(int i = firstEmptyItemIndex; i < MAX_ITEM_COUNT; i++)
        {
            if(null == itemList[firstEmptyItemIndex])
            {
                firstEmptyItemIndex = i;
            }
        }
    }
    public int GetCount()
    {
        return count;
    }

    public int GetGoldAmount()
    {
        return goldAmount;
    }

    public Item GetSelctedItem()
    {
        return selectedItem;
    }

    public void SetSelectedItem(Item item)
    {
        selectedItem = item;
    }

    public int GetSelectedItemIndex()
    {
        return selectedItemIndex;
    }

    public void SetSelectedItemIndex(int selectedItemIndex)
    {
        this.selectedItemIndex = selectedItemIndex;
    }

    public void SellStackableItem(Item item, int amount)
    {
        
        IStackableItem stackableItem = item as IStackableItem;
        int originalAmount = stackableItem.GetAmount();
        if(originalAmount == amount)
        {
            goldAmount += stackableItem.GetTotalPrice();

            //update index
            firstEmptyItemIndex = Math.Min(selectedItemIndex, firstEmptyItemIndex);
            
            //update link
            if(null != stackableItem.GetPrevSameItem())
            {
                IStackableItem prevStameItem = stackableItem.GetPrevSameItem();
                prevStameItem.SetNextSameItem(stackableItem.GetNextSameItem());

            }
            if(null != stackableItem.GetNextSameItem())
            {
                IStackableItem nextSameItem = stackableItem.GetNextSameItem();
                nextSameItem.SetPrevSameItem(stackableItem.GetPrevSameItem());
                
            }
            //destory
            itemList[selectedItemIndex] = null;
            OnItemChanged?.Invoke(this, new OnItemChangedArgs { index = selectedItemIndex}) ;
            selectedItemIndex = -1;
        }
        else
        {
            goldAmount += stackableItem.GetTotalPrice();
            stackableItem.SetAmount(originalAmount - amount);      
        }
        
    }

    public void SellEquipmentItem(Item item)
    {
        goldAmount += item.GetPrice();

        //reset index
        firstEmptyItemIndex = Math.Min(selectedItemIndex, firstEmptyItemIndex);

        //destroy
        itemList[selectedItemIndex] = null;
        OnItemChanged?.Invoke(this, new OnItemChangedArgs { index = selectedItemIndex});
        selectedItemIndex = -1;
    }

    public void Save()
    {
        
	}

	public void Load()
    {
        
	}


}
    
