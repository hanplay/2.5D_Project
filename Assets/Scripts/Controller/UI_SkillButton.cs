using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillButton : MonoBehaviour, IPointerDownHandler
{
    public event EventHandler OnButtonPressed;
    

    private SkillState skillState;
    private Image SkillImage;
    private Image skillBlockerImage;
    private Color blockColor = new Color(0f, 0f, 0f, 0.5f);

    public class SkillButtonDownEventArgs : EventArgs
    {
        public SkillState ButtonDownSkillState;
    }
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
   
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnButtonPressed?.Invoke(this, new SkillButtonDownEventArgs{ ButtonDownSkillState = skillState });
    }



    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void ShowIfSkillStateExist()
    {
        if (null != skillState)
            Show();
    }
    
    public void SetSkillState(SkillState skillState)
    {
        this.skillState = skillState;
    }
}
