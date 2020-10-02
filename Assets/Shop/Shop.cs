using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "Shop/Shop")]
public class Shop : ScriptableObject
{
    [SerializeField]
    private Inventory playerInventory;

    [SerializeField]
    private List<ItemDatum> itemDatumList;

    private int selectedItemIndex = -1;

    private void OnEnable()
    {
        Selector.GetInstance().OnSelect += Selector_OnSelect;
        Selector.GetInstance().OnUnSelect += Selector_OnUnSelect;
    }

    private void Selector_OnUnSelect(object sender, EventArgs e)
    {
        selectedItemIndex = -1;
    }

    private void Selector_OnSelect(object sender, EventArgs e)
    {
        Selector.OnSelectEventArgs onSelectEventArgs = e as Selector.OnSelectEventArgs;
        UI_ShopItem uI_ShopItem = onSelectEventArgs.selectableObject as UI_ShopItem;
        if (null != uI_ShopItem)
        {
            selectedItemIndex = uI_ShopItem.GetIndex();
        }
    }

    public bool IsPlayerAffordable(ItemDatum itemDatum, int amount)
    {
        return playerInventory.IsAffordable(itemDatum, amount);
    }

    public bool IsPlayerAffordable(ItemDatum itemDatum)
    {
        return playerInventory.IsAffordable(itemDatum);
    }

    public bool PlayerTryToBuyStackableItem(ItemDatum itemDatum, int amount)
    {
        return playerInventory.TryToBuyStackableItem(itemDatum, amount);
    }

    public bool PlayerTryTuBuyEquipmentItem(ItemDatum itemDatum)
    {
        return playerInventory.TryToBuyEquipmentItem(itemDatum);
    }

    public void PlayerSellStackableItem(Item item, int amount)
    {
        playerInventory.SellStackableItem(item, amount);
    }

    public void PlayerSellEquipmentItem(Item item)
    {
        playerInventory.SellEquipmentItem(item);
    }

    public ItemDatum GetItemDatum(int index)
    {
        return itemDatumList[index];
    }

    public int GetItemDatumCount()
    {
        return itemDatumList.Count;
    }

    public int GetSelectedItemIndex()
    {
        return selectedItemIndex;
    }

    public void SetSelectedItemIndex(int index)
    {
        selectedItemIndex = index;
    }
}
