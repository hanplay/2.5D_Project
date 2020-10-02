using UnityEngine;
using UnityEngine.EventSystems;
using GameUtility;

public  class UI_InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UI_Inventory uI_Inventory;
    private UI_SelectionFrame uI_SelectionMark;

    private void Awake()
    {
        uI_Inventory = GetComponentInParent<UI_Inventory>();
        uI_SelectionMark = uI_Inventory.GetUISelectionFrame();
    }

    private int index;
    public void SetIndex(int index)
    {
        this.index = index;
    }
    public int GetIndex()
    {
        return index;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (true == Util.IsMultiTouch(eventData))
        {
            return;
        }
        if (null != eventData.pointerDrag)
        {
            UI_Item uI_Item = eventData.pointerDrag.gameObject.GetComponent<UI_Item>();
            if(null != uI_Item)
            {
                uI_Inventory.Swap(index, uI_Item.GetIndex());

                UI_Item renewedUI_Item = transform.GetChild(0).GetComponent<UI_Item>();
                Selector.GetInstance().OnSelectInvoke(renewedUI_Item);
                uI_SelectionMark.ShowGlowingFrame(transform);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(true == Util.IsMultiTouch(eventData))
        {
            return;
        }        
        if (null != eventData.pointerDrag)
        {
            UI_Item uI_Item = eventData.pointerDrag.gameObject.GetComponent<UI_Item>();
            if (null != uI_Item)
            {
                uI_SelectionMark.ShowGlowingFrame(transform);
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
            UI_Item uI_Item = eventData.pointerDrag.gameObject.GetComponent<UI_Item>();
            if (null != uI_Item)
            {
                uI_Inventory.LightOffUI_InventorySlot();
            }
        }
    }
}
