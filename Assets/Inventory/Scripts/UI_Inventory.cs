using UnityEngine;
using System.Collections.Generic;

public class UI_Inventory : MonoBehaviour
{
	[SerializeField]
	private Inventory inventory;

	private Transform uI_ItemTemplate;
	private Transform uI_InventorySlotTemplate;

	private UI_DraggingItem uI_DraggingItem;
	private UI_SelectionFrame uI_SelectionFrame;

	private TMPro.TextMeshProUGUI uI_GoldAmountText;


	private const float X_START = 45f;
	private const float Y_START = -50f;
	private const float SPACE_BETWEEN_ITEM = 55f;
	private const int COLUMN_NUM = 5;
	private const int ROW_NUM = 5;

	private List<UI_InventorySlot> uI_InventorySlotList = new List<UI_InventorySlot>();


	private void Awake()
	{
		uI_InventorySlotTemplate = transform.Find("UI_InventorySlot");
		uI_InventorySlotTemplate.gameObject.SetActive(false);

		uI_ItemTemplate = transform.Find("UI_Item");
		uI_ItemTemplate.gameObject.SetActive(false);

		uI_GoldAmountText = transform.Find("UI_GoldAmount").GetComponentInChildren<TMPro.TextMeshProUGUI>();
		uI_DraggingItem = transform.Find("UI_DraggingItem").GetComponent<UI_DraggingItem>();
		uI_DraggingItem.gameObject.SetActive(false);

		uI_SelectionFrame = transform.Find("UI_SelectionFrame").GetComponent<UI_SelectionFrame>();
		uI_SelectionFrame.gameObject.SetActive(false);


		inventory.OnItemChanged += Inventory_OnItemChanged;
	}

	private void Inventory_OnItemChanged(object sender, System.EventArgs e)
	{
		Inventory.OnItemChangedArgs onItemChangedArgs = e as Inventory.OnItemChangedArgs;
	}

	private void Start()
	{
		CreateDisplay();
	}

	private void Inventory_OnReload(object sender, System.EventArgs e)
	{
		foreach (Transform child in transform)
		{
			Destroy(child);
		}
		CreateDisplay();
	}

	private void CreateDisplay()
	{
		for (int i = 0; i < Inventory.MAX_ITEM_COUNT; i++)
		{

			UI_InventorySlot uI_InventorySlot = CreateUI_InventorySlot(i);
			uI_InventorySlotList.Add(uI_InventorySlot);

			if (null != inventory.GetItem(i))
			{
				CreateUI_Item(i);
			}
		}
	}

	private Vector2 UI_InventorySlotPositionInInvnetory(int index)
	{
		int row = index / COLUMN_NUM;
		int column = index % ROW_NUM;
		return new Vector2(X_START + SPACE_BETWEEN_ITEM * column, Y_START - SPACE_BETWEEN_ITEM * row);
	}

	private UI_InventorySlot CreateUI_InventorySlot(int index)
	{
		Transform uI_InventorySlotTransform = Instantiate(uI_InventorySlotTemplate, transform);
		uI_InventorySlotTransform.GetComponent<RectTransform>().anchoredPosition = UI_InventorySlotPositionInInvnetory(index);
		uI_InventorySlotTransform.gameObject.SetActive(true);

		UI_InventorySlot uI_InventorySlot = uI_InventorySlotTransform.GetComponent<UI_InventorySlot>();
		uI_InventorySlot.SetIndex(index);

		return uI_InventorySlot;
	}

	private void UpdateUI_GoldAmountText()
	{
		uI_GoldAmountText.text = inventory.GetGoldAmount().ToString();
	}

	public UI_InventorySlot GetSelectedUI_InventorySlot()
	{		
		if(-1 == GetSelectedItemIndex())
		{
			return null;
		}
		else
		{
			return uI_InventorySlotList[GetSelectedItemIndex()];			
		}
	}

	

	public void LightOffUI_InventorySlot()
	{
		uI_SelectionFrame.Hide();
	}

	private UI_Item CreateUI_Item(int index)
	{
		UI_InventorySlot uI_InventorySlot = uI_InventorySlotList[index];
		Transform uI_ItemTransform = Instantiate(uI_ItemTemplate, uI_InventorySlot.transform);
		uI_ItemTransform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		uI_ItemTransform.gameObject.SetActive(true);

		UI_Item uI_Item = uI_ItemTransform.GetComponent<UI_Item>();
		uI_Item.SetIndex(index);
		uI_Item.SetItem(inventory.GetItem(index));
		uI_Item.UpdateUI();

		return uI_Item;
	}

	public void UpdateUI_Item(int index)
	{
		if(null != inventory.GetItem(index))
		{
			UI_Item uI_Item = uI_InventorySlotList[index].GetComponentInChildren<UI_Item>();
			if(null != uI_Item)
			{
				uI_Item.UpdateItem(inventory.GetItem(index)); 
			}
			else
			{
				CreateUI_Item(index).transform.SetAsFirstSibling();
			}							
		}
		else
		{
			UI_Item uI_Item = uI_InventorySlotList[index].GetComponentInChildren<UI_Item>();
			if (null != uI_Item)
			{
				Destroy(uI_Item.gameObject);
			}
		}		
	}

	public void SetInventoryItem(int index, Item item)
	{
		inventory.SetItem(index, item);
	}

	public Item GetInventoryItem(int index)
	{
		return inventory.GetItem(index);
	}

	public int GetSelectedItemIndex()
	{
		return inventory.GetSelectedItemIndex();
	}

	public void SetSelectedItemIndex(int index)
	{
		
	}



	public void Swap(int index1, int index2)
	{
		Item tmpItem = inventory.GetItem(index1);
		inventory.SetItem(index1, inventory.GetItem(index2));
		inventory.SetItem(index2, tmpItem);

		UpdateUI_Item(index1);
		UpdateUI_Item(index2);
	}

	public UI_DraggingItem GetUI_DraggingItem()
	{
		return uI_DraggingItem;
	}

	public UI_SelectionFrame GetUISelectionFrame()
	{
		return uI_SelectionFrame;
	}
}