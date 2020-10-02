using System.Collections.Generic;
using UnityEngine;


public class UI_Shop : MonoBehaviour
{
	[SerializeField]
	private Shop shop;

	private List<UI_ShopItem> uI_ShopItemList = new List<UI_ShopItem>();

	private Transform uI_ShopItemTemplate;
	private Transform content;

	private UI_SelectionFrame uI_SelectionFrame;

	private const float SPACE_BETWEEN_ITEM = 50f;


	private void Awake()
	{
		content = transform.Find("ViewPort").Find("Content");
		uI_ShopItemTemplate = content.Find("UI_ShopItem");
		uI_ShopItemTemplate.gameObject.SetActive(false);

		uI_SelectionFrame = transform.Find("UI_SelectionFrame").GetComponent<UI_SelectionFrame>();
		uI_SelectionFrame.gameObject.SetActive(false);
	}

	private void Start()
	{
		CreateDisplay();
	}
	
	private void CreateDisplay()
	{
		int count = shop.GetItemDatumCount();
		int i;
		for (i = 0; i < count; i++)
		{
			Transform uI_ShopItemTransform = Instantiate(uI_ShopItemTemplate, content);
			uI_ShopItemTransform.gameObject.SetActive(true);
			uI_ShopItemTransform.GetComponent<RectTransform>().anchoredPosition = UI_ShopItemPositionInUI_Shop(i);
			
			UI_ShopItem uI_ShopItem = uI_ShopItemTransform.GetComponent<UI_ShopItem>();
			uI_ShopItem.UpdateItemDatum(shop.GetItemDatum(i));
			uI_ShopItem.SetIndex(i);
			uI_ShopItemList.Add(uI_ShopItem);


		}
		content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, SPACE_BETWEEN_ITEM * i ); 	
	}

	public void CustumerBuy(int index)
	{
		ItemDatum itemDatum = shop.GetItemDatum(index);
		if(itemDatum.IsStackable())
		{
			CustumerBuyStackableItem(itemDatum);
		}
		else
		{
			CustumerBuyEquipmentItem(itemDatum);
		}
	}

	private void CustumerBuyStackableItem(ItemDatum itemDatum)
	{
		UI_InputWindow.Show_Static("몇개를 구매하시겠습니까?", "1234567890",
		(int amount) =>
		{
			if (true == shop.IsPlayerAffordable(itemDatum, amount))
			{
				shop.PlayerTryToBuyStackableItem(itemDatum, amount);
				return;

			}
			else
			{
				//pop up buying failure message
				Debug.Log("Stackable buy Failure");
				return;
			}
		});
	}

	private void CustumerBuyEquipmentItem(ItemDatum itemDatum)
	{
		if (true == shop.IsPlayerAffordable(itemDatum))
		{
			UI_PopupWindow.Show_Static("정말로 구매하시겠습니까?",
			() =>
			{
				if (true == shop.PlayerTryTuBuyEquipmentItem(itemDatum))
				{

				}
				else
				{
					//pop up window inventory is full so that cannot buy it
				}

			});
		}
		else
		{
			//돈 부족 구매 불가 pop up
			Debug.Log("Lack of Money: buy Failure");
		}

	}

	public void CustumerSell(Item item)
	{
		if (item.IsStackable())
		{
			CustumerSellStackableItem(item);
		}
		else
		{
			CustumerSellEquipmemtItem(item);
		}
	}

	private void CustumerSellStackableItem(Item item)
	{
		UI_InputWindow.Show_Static("몇개를 판매하시겠습니까?", "1234567890",
		(int amount) =>
		{
			IStackableItem stackableItem = item as IStackableItem;
			if (stackableItem.GetAmount() >= amount)
			{
				shop.PlayerSellStackableItem(item, amount);
				return;

			}
			else
			{
				//pop up buying failure message
				Debug.Log("Stackable Sell Failure");
				return;
			}
		});
	}

	private void CustumerSellEquipmemtItem(Item item)
	{

		UI_PopupWindow.Show_Static("정말로 판매하시겠습니까?",
		() =>
		{
			shop.PlayerSellEquipmentItem(item);

		});
	}


	private Vector2 UI_ShopItemPositionInUI_Shop(int index)
	{
		return new Vector2(0f, -SPACE_BETWEEN_ITEM * index);
	}


	public UI_SelectionFrame GetUI_SelectionFrame()
	{
		return uI_SelectionFrame;
	}
}
