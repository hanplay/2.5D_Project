using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameUtility;


public class UI_Item : MonoBehaviour, ISelectable, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private UI_Inventory uI_Inventory;
	[SerializeField]
	private Item item;

	private TMPro.TextMeshProUGUI text;
	
	private int index;

	private void Awake()
	{
		text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
	}

	public void UpdateUI()
	{
		GetComponent<Image>().sprite = item.GetSprite();
		UpdateText();
	}

	public void UpdateItem(Item item)
	{
		this.item = item;
		UI_InventorySlot uI_InventorySlot = transform.parent.GetComponent<UI_InventorySlot>();
		if(null != uI_InventorySlot)
		{
			index = uI_InventorySlot.GetIndex();
		}
		else
		{
			Debug.LogError("Error: uI_Item parent is not UI_InventorySlot");
			Debug.Break();
		}

		UpdateUI();
	}

	private void UpdateText()
	{
		if (item.IsStackable())
		{
			IStackableItem stackableItem = item as IStackableItem;
			text.SetText(stackableItem.GetAmount().ToString());
		}
		else
		{
			text.SetText("");
		}
	}
	
	public Item GetItem()
	{
		return item;
	}

	public void SetItem(Item item)
	{
		this.item = item;
	}

	public int GetIndex()
	{
		return index;
	}
	public void SetIndex(int index)
	{
		this.index = index;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (true == Util.IsMultiTouch(eventData))
		{
			return;
		}
		UI_DraggingItem uI_DraggingItem = uI_Inventory.GetUI_DraggingItem();
		uI_DraggingItem.Show();
		uI_DraggingItem.transform.position = eventData.position;
		uI_DraggingItem.SetImageSprite(item.GetSprite());

		Selector.GetInstance().OnSelectInvoke(this);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (true == Util.IsMultiTouch(eventData))
		{
			return;
		}

		uI_Inventory.GetUI_DraggingItem().Hide();
		uI_Inventory.GetUISelectionFrame().ShowGlowingFrame(transform.parent);
		
	}
	public void OnBeginDrag(PointerEventData eventData)
	{
		if (true == Util.IsMultiTouch(eventData))
		{
			return;
		}
		uI_Inventory.GetUI_DraggingItem().transform.position = eventData.position;

	}

	public void OnDrag(PointerEventData eventData)
	{
		if (true == Util.IsMultiTouch(eventData))
		{
			return;
		}
		uI_Inventory.GetUI_DraggingItem().transform.position = eventData.position;
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (true == Util.IsMultiTouch(eventData))
		{
			return;
		}
		if (null != eventData.pointerDrag)
		{
			UI_Item uI_Item = eventData.pointerDrag.GetComponent<UI_Item>();
			if(null != uI_Item)
			{
				uI_Inventory.Swap(index, uI_Item.GetIndex());

				uI_Inventory.GetUISelectionFrame().ShowGlowingFrame(transform);
				Selector.GetInstance().OnSelectInvoke(this);
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (true == Util.IsMultiTouch(eventData))
		{
			return;
		}
		if (null != eventData.pointerDrag)
		{
			UI_Item uI_Item = eventData.pointerDrag.GetComponent<UI_Item>();
			if (null != uI_Item)
			{
				uI_Inventory.GetUISelectionFrame().ShowGlowingFrame(transform);
			}
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (true == Util.IsMultiTouch(eventData))
		{
			return;
		}
		if (null != eventData.pointerDrag)
		{
			UI_Item uI_Item = eventData.pointerDrag.GetComponent<UI_Item>();
			if (null != uI_Item)
			{
				uI_Inventory.LightOffUI_InventorySlot();
			}
		}
	}

}