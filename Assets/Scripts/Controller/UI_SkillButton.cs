using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillButton : MonoBehaviour, IPointerDownHandler
{

    private Skill skill;
    private Image skillImage;
    private Image skillBlockerImage;
    private Color blockColor = new Color(0f, 0f, 0f, 0.5f);

    void Awake()
    {
        skillImage = GetComponent<Image>();
        skillBlockerImage = transform.Find("UI_SkillBlocker").GetComponent<Image>();
        skillBlockerImage.color = blockColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (null == skill)
            return;
        if(skill.IsCoolDownTime())
        {
            skillBlockerImage.fillAmount = skill.GetRemainingCoolDownTimeProportion();
        }
   
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(skill.IsTargetSkill())
        {
            skill.OrderPlayerTargetSkillCommand();
        }
        else
        {
            skill.OrderPlayerBasicSkillCommand();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void SetSkill(Skill skill)
    {
        this.skill = skill;
        skillImage.sprite = skill.GetSkillSprite();
    }

}
