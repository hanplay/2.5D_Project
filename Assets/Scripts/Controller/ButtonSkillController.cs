using System.Collections.Generic;
using UnityEngine;

public class ButtonSkillController : MonoBehaviour
{
    //Singleton
    private static ButtonSkillController instance;
    public static ButtonSkillController GetInstance()
    {
        return instance;
    }

    private Player player;
    private List<UI_SkillButton> uI_SkillButtonList = new List<UI_SkillButton>();

    private void Awake()
    {
        instance = this;
        foreach (Transform childTransform in transform)
        {
            uI_SkillButtonList.Add(childTransform.GetComponent<UI_SkillButton>());
        }
        uI_SkillButtonList.TrimExcess();

    }


    private void Player_OnDead(object sender, System.EventArgs e)
    {
        player = null;
        for(int i = 0; i < uI_SkillButtonList.Count; i++)
        {
            uI_SkillButtonList[i].Hide();
        }
    }

    private void BindPlayerSkillsToUI_SkillButtonsAndShow()
    {
        for(int i = 0; i < player.GetSkillCount(); i++)
        {
            if(null == player.GetSkill(i))
            {
                uI_SkillButtonList[i].Hide();
            }
            else
            {
                uI_SkillButtonList[i].SetSkill(player.GetSkill(i));
                uI_SkillButtonList[i].Show();
            }
        }

    }

    public void SetPlayer(Player player)
    {
        if(null != this.player)
        {
            this.player.OnDead -= Player_OnDead;
        }
        player.OnDead += Player_OnDead;

        this.player = player;
        BindPlayerSkillsToUI_SkillButtonsAndShow();
    }
}
