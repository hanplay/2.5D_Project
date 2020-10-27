using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{

    private Player player;
    private ButtonSkillController buttonSkillController;
    private BuffShower buffShower;

    private static PlayerSelector instance;
    public static PlayerSelector GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        buttonSkillController = transform.Find("ButtonController").GetComponent<ButtonSkillController>();
        buffShower = transform.Find("BuffShower").GetComponent<BuffShower>();
    }

    public void SetPlayer(Player player)
    {
        if (null != this.player)
        {
            this.player.OnDieEvent -= Player_OnDieEvent;
        }
        player.OnDieEvent += Player_OnDieEvent;

        this.player = player;
        buttonSkillController.BindPlayerSkillsToUI_SkillButtonsAndShow(player);
        buffShower.SetBuffSystem(player.GetBuffSystem());
    }

    private void Player_OnDieEvent(object sender, System.EventArgs e)
    {
        player = null;
        buttonSkillController.HideAllUI_SkillButtons();
        buffShower.UnBindBuffSystem();
    }
}
