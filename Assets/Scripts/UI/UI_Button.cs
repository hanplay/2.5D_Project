using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private Sprite sprite;

	public Action Click;

	public void OnPointerClick(PointerEventData eventData)
	{
		Click?.Invoke();
	}

	public void SetSprite(Sprite sprite)
	{
		this.sprite = sprite;
	}
}
