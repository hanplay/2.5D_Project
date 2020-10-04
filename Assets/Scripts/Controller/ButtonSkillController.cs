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
        // start
    }

    private void Player_OnDead(object sender, System.EventArgs e)
    {
        player = null;
        for(int i = 0; i < uI_SkillButtonList.Count; i++)
        {
            uI_SkillButtonList[i].Hide();
        }
        
    }

}
