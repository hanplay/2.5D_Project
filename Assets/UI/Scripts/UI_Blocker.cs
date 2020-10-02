using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Blocker : MonoBehaviour, IPointerDownHandler
{
	private static UI_Blocker instnace;
	private IPopupWindow popupWindow; 

	private void Awake()
	{
		instnace = this;
	}

	public void Show(IPopupWindow popupWindow)
	{
		this.popupWindow = popupWindow;
		gameObject.SetActive(true);
		transform.SetAsLastSibling();
	}

	public void Hide()
	{
		popupWindow = null;
		gameObject.SetActive(false);
		
	}

	public static UI_Blocker GetInstance()
	{
		return instnace;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		popupWindow.Hide();
	}
}
