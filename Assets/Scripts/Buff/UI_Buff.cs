using UnityEngine;
using UnityEngine.UI;

public class UI_Buff : MonoBehaviour
{
    private Buff buff;
    private Image uI_Blocker;
    private void Awake()
    {
        uI_Blocker = transform.Find("UI_Blocker").GetComponent<Image>();
        TimedBuff timedBuff = buff as TimedBuff;
        if (null != timedBuff)
        {
            timedBuff.OnLagTimeChange += TimedBuff_OnLagTimeChange;
        }
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


}
