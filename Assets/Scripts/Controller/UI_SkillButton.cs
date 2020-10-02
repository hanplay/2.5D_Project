using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public event EventHandler OnButtonPressed;
    
    private bool isCoolDownTime = false;
    private Image skillBlockerImage;
    private Color blockColor = new Color(0f, 0f, 0f, 0.5f);
    private float coolDownTime = 5f;
    private float lagTime;

    void Awake()
    {
        skillBlockerImage = transform.Find("UI_SkillBlocker").GetComponent<Image>();
        skillBlockerImage.enabled = false;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (false == isCoolDownTime)
            return;

        lagTime += Time.deltaTime;
        skillBlockerImage.fillAmount = 1f - lagTime / coolDownTime;
        if(lagTime >= coolDownTime)
        {
            lagTime = 0f;
            EndCoolSkillCoolDownTime();            
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        print("OnPointerDown");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        print("OnPointerUp");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("OnPointerClick");
        //if (isCoolDownTime)
        //    return;
        ActivateSkill();

    }

    private void ActivateSkill()
    {
        skillBlockerImage.enabled = true;
        skillBlockerImage.color = blockColor;
        isCoolDownTime = true;
    }

    private void EndCoolSkillCoolDownTime()
    {
        skillBlockerImage.enabled = false;
        skillBlockerImage.color = Color.white;
        isCoolDownTime = false;
    }

    
}
