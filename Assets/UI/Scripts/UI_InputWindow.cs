using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputWindow : MonoBehaviour, IPopupWindow
{
	private static UI_InputWindow instance;
	private UI_Button acceptButton;
	private UI_Button cancelButton;
	private Text popupText;
	private TMPro.TMP_InputField inputField;
	

	private void Awake()
	{
		instance = this;
		acceptButton = transform.Find("acceptButton").GetComponent<UI_Button>();
		cancelButton = transform.Find("cancelButton").GetComponent<UI_Button>();
		popupText = transform.Find("popupText").GetComponent<Text>();
		inputField = transform.Find("inputField").GetComponent<TMPro.TMP_InputField>();
	}

	private void Show(string popupTextString, string validateCharacters, Action<int> accept, Action cancel)
	{
		UI_Blocker.GetInstance().Show(this);
		gameObject.SetActive(true);
		transform.SetAsLastSibling();

		popupText.text = popupTextString;

		inputField.characterLimit = 2;
		inputField.onValidateInput =
		(string text, int charIndex, char addedChar) =>
		{
			return ValidateChar(validateCharacters, addedChar);
		};

		int amount = int.Parse(inputField.text);

		acceptButton.Click = () => { accept(amount); };
		cancelButton.Click = () => { cancel(); };
	}

	private void Show(string popupTextString, string validateCharacters, Action<int> accept)
	{
		Show(popupTextString, validateCharacters, accept);
		cancelButton.Click = () => { Hide(); };
	}

	public void Hide()
	{
		UI_Blocker.GetInstance().Hide();
		gameObject.SetActive(false);
	}

	public static void Show_Static(string popupTextString, string validateCharacters, Action<int> accept, Action cancel)
	{
		instance.Show(popupTextString, validateCharacters, accept, cancel);
	}

	public static void Show_Static(string popupTextString, string validateCharacters, Action<int> accept)
	{
		instance.Show(popupTextString, validateCharacters, accept);
	}

	public static void Hide_Static()
	{
		instance.Hide();
	}

	private char ValidateChar(string validateCharacters, char addedChar)
	{
		//만약 validateCharacters에 addedChar이 없을 경우
		if(-1 == validateCharacters.IndexOf(addedChar))
		{
			return '\0';
		}
		//else
		else
		{
			return addedChar;
		}
	}

}
