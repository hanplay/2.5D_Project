 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{

    private Player player;
    private ButtonSkillController buttonSkillController;
    private BuffShower buffShower;

    public static PlayerSelector Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
        buttonSkillController = transform.Find("ButtonController").GetComponent<ButtonSkillController>();
        buffShower = transform.Find("BuffShower").GetComponent<BuffShower>();
    }

    public void SetPlayer(Player player)
    {
        if (null != this.player)
        {
            this.player.HideSelectCircle();
        }

        this.player = player;
        player.ShowSelectCircle();
        buttonSkillController.BindPlayerSkillsToUI_SkillButtonsAndShow(player);
        buffShower.SetBuffSystem(player.GetBuffSystem());
    }

    private void Player_OnDieEvent(object sender, System.EventArgs e)
    {
        player.HideSelectCircle();
        player = null;
        buttonSkillController.HideAllUI_SkillButtons();
        buffShower.UnBindBuffSystem();
    }
}
