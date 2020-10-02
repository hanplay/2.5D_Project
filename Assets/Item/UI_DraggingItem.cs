using UnityEngine;
using UnityEngine.UI;

public class UI_DraggingItem : MonoBehaviour
{
    private Image image;
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetImageSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void Show()
    {
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
    }

    public void Show(Vector3 position)
    {        
        transform.SetAsLastSibling();
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


}
