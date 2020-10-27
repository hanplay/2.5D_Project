using UnityEngine;
using UnityEngine.UI;

public class UI_Buff : MonoBehaviour
{
    private Image buffImage;
    private Image uI_Blocker;
    private void Awake()
    {
        buffImage = GetComponent<Image>();
        uI_Blocker = transform.Find("UI_Blocker").GetComponent<Image>();
    }

    private void TimedBuff_OnLagTimeChange(float proportion)
    {
        uI_Blocker.fillAmount = proportion;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetBuff(Buff buff)
    {
        buffImage.sprite = buff.GetBuffSprite();
        
        TimedBuff timedBuff = buff as TimedBuff;
        if (null != timedBuff)
        {
            timedBuff.OnLagTimeChange += TimedBuff_OnLagTimeChange;
        }
    }

}
