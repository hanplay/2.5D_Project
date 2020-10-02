using UnityEngine;
using UnityEngine.UI;

public class UI_SelectionFrame : MonoBehaviour
{	
	private RectTransform rectTransform;
		
	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void ShowGlowingFrame(Transform otherTransform)
	{
		Show();
		transform.SetParent(otherTransform);
		transform.position = otherTransform.position;
		transform.SetAsLastSibling();
	}


}
