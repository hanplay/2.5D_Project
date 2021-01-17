using UnityEngine;
using UnityEngine.UI;

public class UI_Buff : MonoBehaviour
{
    private Image buffImage;
    private Image uI_Blocker;
    private TMPro.TextMeshProUGUI textMesh;
    private Buff buff;
    //private TimedBuff timedBuff;


    private void Awake()
    {
        buffImage = GetComponent<Image>();
        uI_Blocker = transform.Find("UI_Blocker").GetComponent<Image>();
        textMesh = transform.Find("IndexNumber").GetComponent<TMPro.TextMeshProUGUI>();
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
        this.buff = buff;
        buffImage.sprite = buff.GetBuffSprite();       
    }

    private void Update()
    {
        TimedBuff timedBuff = buff as TimedBuff;
        if (null == timedBuff)
            uI_Blocker.fillAmount = 0f;
        else
            uI_Blocker.fillAmount = timedBuff.GetRemainingTimeProportion();


        if (Buff.DoNotShow == buff.IndexNumber())
        {
            if (0 == textMesh.text.Length)
                return;
            textMesh.text = "";
        }
        else
        {
            textMesh.text = buff.IndexNumber().ToString();
        }
    }



}
