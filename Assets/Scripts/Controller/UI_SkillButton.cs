using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillButton : MonoBehaviour, IPointerDownHandler
{

    private ButtonSkillController buttonSkillController;
    private Skill skill;
    private Image skillImage;
    private Image skillBlockerImage;
    private Color blockColor = new Color(0f, 0f, 0f, 0.5f);
    private Color glowColor = new Color(0.3773585f, 0.2277012f, 0f);

    private enum GlowState
    {
        Glow,
        None
    }

    private GlowState glowState;

    void Awake()
    {
        buttonSkillController = transform.parent.GetComponent<ButtonSkillController>();
        skillImage = GetComponent<Image>();
        skillBlockerImage = transform.Find("UI_SkillBlocker").GetComponent<Image>();
        skillBlockerImage.color = blockColor;
        Hide();
    }

    void Start()
    {
        skillImage.material = new Material(GameAssets.Instance.UnitMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        if (null == skill)
            return;
        if(skill.IsChasing())
        {
            if(GlowState.None == glowState)
            {
                Glow(glowColor);
                glowState = GlowState.Glow;
            }
        }
        else
        {
            if (GlowState.Glow == glowState)
            {
                Glow(Color.black);
                glowState = GlowState.None;
            }
        }


        if(skill.IsCoolDownTime())
        {
            UpdateSkillImageRemainingProportion();
        }
   
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonSkillController.ActivateSkill(skill);
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

    public void Glow(Color color)
    {
        skillImage.material.color = color;
    }

    public void UpdateSkillImageRemainingProportion()
    {
        skillBlockerImage.fillAmount = skill.GetRemainingCoolDownTimeProportion();
    }
}
