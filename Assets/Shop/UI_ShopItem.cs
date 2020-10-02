using GameUtility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ShopItem : MonoBehaviour, ISelectable, IDropHandler, IPointerDownHandler
{
	[SerializeField]
	private UI_Shop uI_Shop;
	[SerializeField]
	ItemDatum itemDatum;

	private Image image;
	private TMPro.TextMeshProUGUI priceText;
	
	private int index;

	private float doubleClickInterval;
	private float lastClickTime;


	private void Awake()
	{
		doubleClickInterval = 0.5f;
		lastClickTime =  - doubleClickInterval;

		image = transform.Find("Image").GetComponent<Image>();
		priceText = transform.Find("PriceText").GetComponent<TMPro.TextMeshProUGUI>();
	}


	public void OnPointerDown(PointerEventData eventData)
	{
		if(Util.IsMultiTouch(eventData))
		{
			return;
		}

		//더블 클릭
		if(Time.time - lastClickTime < doubleClickInterval)
		{
			lastClickTime = 0;
			//custumer Buy function call
			Debug.Log(typeof(UI_ShopItem).ToString() + " double: " + index);
		}
		//일반 클릭
		else
		{
			lastClickTime = Time.time;
			uI_Shop.GetUI_SelectionFrame().ShowGlowingFrame(transform);
			Selector.GetInstance().OnSelectInvoke(this);
			Debug.Log(typeof(UI_ShopItem).ToString() + ": " + index);
		}
	}


	public void OnDrop(PointerEventData eventData)
	{
		RaycastResult raycastResult = eventData.pointerCurrentRaycast;
		if(null != raycastResult.gameObject)
		{
			UI_Item uI_Item = raycastResult.gameObject.GetComponent<UI_Item>();
			if(null != uI_Item)
			{
				//custumer sell function call	

			}
		}
	}

	public int GetPrice()
	{
		return itemDatum.GetPrice();
	}

	public void UpdateItemDatum(ItemDatum itemDatum)
	{
		this.itemDatum = itemDatum;
		UpdateUI();
	}


	private void UpdateUI()
	{
		image.sprite = itemDatum.GetSprite();
		priceText.SetText("" + itemDatum.GetPrice());
	}

	public void SetIndex(int index)
	{
		this.index = index;
	}

	public int GetIndex()
	{
		return index;
	}


}
