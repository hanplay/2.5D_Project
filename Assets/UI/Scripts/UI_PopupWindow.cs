using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_PopupWindow : MonoBehaviour, IPopupWindow
{
	private static UI_PopupWindow instance;
	private UI_Button acceptButton;
	private UI_Button cancelButton;
	private Text popupText;

	private void Awake()
	{
		instance = this;
		acceptButton = transform.Find("acceptButton").GetComponent<UI_Button>();
		cancelButton = transform.Find("cancelButton").GetComponent<UI_Button>();
		popupText = transform.Find("popupText").GetComponent<Text>();
		
	}

	private void Show(string popupTextString, Action accept, Action cancel)
	{
		UI_Blocker.GetInstance().Show(this);
		gameObject.SetActive(true);
		transform.SetAsLastSibling();

		popupText.text = popupTextString;

		acceptButton.Click = () => { accept(); };
		cancelButton.Click = () => { cancel(); };
	}

	private void Show(string popupTextString, Action accept)
	{
		Show(popupTextString, accept, ()=> { });
	}
	
	public void Hide()
	{
		UI_Blocker.GetInstance().Hide();
		gameObject.SetActive(false);
	}

	public static void Show_Static(string popupTextString, Action accept, Action cancel)
	{
		instance.Show(popupTextString, accept, cancel);
	}

	public static void Show_Static(string popupTextString, Action accept)
	{
		instance.Show(popupTextString, accept);
	}

	public static void Hide_Static()
	{
		instance.Hide();
	}
}
