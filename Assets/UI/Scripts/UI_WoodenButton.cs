using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_WoodenButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Sprite buttonSpriteNormal;
    [SerializeField]
    private Sprite buttonSpritePressed;

    private Image buttonImage;
    private RectTransform textRectTransform;
    private Vector2 textOriginPosition;
    private TMPro.TextMeshProUGUI text;


    public event Action Click;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }
    private void Start()
    {        
        textRectTransform = transform.Find("Text").GetComponent<RectTransform>();
        textOriginPosition = textRectTransform.anchoredPosition;
        text = textRectTransform.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = buttonSpriteNormal;
        textRectTransform.anchoredPosition = textOriginPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = buttonSpritePressed;
        textRectTransform.anchoredPosition = MovedPositioinWhenButtonPressed();
        Click?.Invoke();
    }

    private Vector2 MovedPositioinWhenButtonPressed()
    {
        return new Vector2(textOriginPosition.x, textOriginPosition.y - GetComponent<RectTransform>().sizeDelta.y * 2f / 10f);
    }

    public void SetText(string textString)
    {
        text.SetText(textString);
    }

    public void SetClick(Action ClickAction)
    {
        Click = ClickAction;
        
    }

    
}
