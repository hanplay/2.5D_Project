using System.Collections.Generic;
using UnityEngine;
using GameUtility;

public class ButtonSkillController : MonoBehaviour
{
    private MouseInputController mouseInputController;
    private Player player;

    private List<UI_SkillButton> uI_SkillButtonList = new List<UI_SkillButton>();

    private void Awake()
    {
        mouseInputController = GetComponent<MouseInputController>();
        mouseInputController.OnPlayerClicked += MouseInputController_OnPlayerClicked;

        foreach (Transform childTransform in transform)
        {
            uI_SkillButtonList.Add(childTransform.GetComponent<UI_SkillButton>());
        }
        uI_SkillButtonList.TrimExcess();

        for(int i = 0; i < uI_SkillButtonList.Count; i++)
        {
            uI_SkillButtonList[i].OnButtonPressed += ButtonSkillController_OnButtonPressed;
        }

    }

    private void ButtonSkillController_OnButtonPressed(object sender, System.EventArgs e)
    {
        UI_SkillButton.SkillButtonDownEventArgs skillButtonDownEventArgs = e as UI_SkillButton.SkillButtonDownEventArgs;
        player.SetNextState(skillButtonDownEventArgs.ButtonDownSkillState);
    }


    private void MouseInputController_OnPlayerClicked(object sender, System.EventArgs e)
    {
        if(null != player)
        {
            player.OnDead -= Player_OnDead;
        }
        var onPlayerClickedEvent = e as MouseInputController.OnPlayerClickedEvent;
        player =  onPlayerClickedEvent.clickedPlayer;
        player.OnDead += Player_OnDead;

        BindPlayerSkillsToUI_SkillButtons();
        ShowUI_SkillButtons();

    }

    private void Player_OnDead(object sender, System.EventArgs e)
    {
        player = null;
        for(int i = 0; i < uI_SkillButtonList.Count; i++)
        {
            uI_SkillButtonList[i].Hide();
        }
        
    }

    private void BindPlayerSkillsToUI_SkillButtons()
    {
        for(int i = 0; i < player.GetSkillCount(); i++)
        {
            if(null != player.GetSkill(i))
            {
                uI_SkillButtonList[i].SetSkillState(player.GetSkill(i));
            }
        }

    }

    private void ShowUI_SkillButtons()
    {
        for(int i = 0; i < uI_SkillButtonList.Count; i++)
        {
            uI_SkillButtonList[i].ShowIfSkillStateExist();
        }
    }

}
